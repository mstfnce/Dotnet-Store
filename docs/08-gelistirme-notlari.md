# 08 — Geliştirme Notları

Bu doküman, kod okunurken göze çarpan eksikleri ve teknik borcu toplar. Amaç suçlama değil,
"buraya dokunacaksan şunu bil" demektir. Öncelik sütunu şu ölçüye göredir:

| Öncelik | Anlam |
|---|---|
| 🔴 Yüksek | Kullanıcıyı doğrudan etkiler veya para/veri kaybına yol açabilir |
| 🟡 Orta | Canlıya çıkmadan mutlaka ele alınmalı |
| ⚪ Düşük | Temizlik / iyileştirme |

---

## 🔴 `/Home/Error` action'ı yok

`Program.cs` üretim ortamında hataları `/Home/Error` adresine yönlendirir:

```csharp
app.UseExceptionHandler("/Home/Error");
```

Ancak `HomeController` içinde `Error` adında bir action **yoktur** — sınıfta yalnızca
`Index` bulunur. `Views/Shared/Error.cshtml` ve `ErrorViewModel` hazır durmasına rağmen
onları render eden bir action eksiktir. Sonuç: üretimde bir istisna oluştuğunda kullanıcı
anlamlı bir hata sayfası yerine 404 görür.

**Çözüm:** `HomeController` sınıfına klasik `Error` action'ını eklemek:

```csharp
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });
}
```

---

## 🔴 Siparişe yazılan tutar ile çekilen tutar farklı

`OrderController.Checkout` içinde:

```csharp
ToplamFiyat = cart.Toplam();          // AraToplam × 1.2  (KDV dahil)
```

`ProcessPayment` içinde ise:

```csharp
request.Price     = cart.AraToplam().ToString(...);   // KDV hariç
request.PaidPrice = cart.AraToplam().ToString(...);
```

Yani veritabanına KDV **dahil** tutar yazılırken karttan KDV **hariç** tutar çekilir.
Sepet ekranında müşteriye gösterilen tutar da `Toplam()` olduğu için müşteri beklediğinden
farklı bir tutar öder. İkisinin de aynı metodu kullanması gerekir.

---

## 🟡 Ödeme, sipariş kaydından önce alınıyor

Akış şu sırayla ilerliyor:

1. Iyzipay'e ödeme isteği gönderilir
2. Başarılıysa `Orders.Add` + `Carts.Remove` + `SaveChangesAsync`

Adım 2'de bir veritabanı hatası olursa **para çekilmiş ama sipariş kaydedilmemiş** olur ve
bunu telafi edecek bir mekanizma (transaction, iptal/iade çağrısı, kayıt logu) yoktur.

**Çözüm yönü:** ödeme öncesinde siparişi "beklemede" durumuyla kaydetmek, ödeme sonucuna
göre durumu güncellemek. Bunun için `Order` entity'sine bir durum alanı eklenmesi gerekir —
şu an sipariş durumu diye bir kavram modelde yok.

---

## 🟡 `SeedDatabase.Initialize` metodu `async void`

```csharp
public static async void Initialize(IApplicationBuilder app)
```

`async void` olduğu için:

- Çağıran taraf (`Program.cs`) tamamlanmasını **bekleyemez**; uygulama seed bitmeden
  istek almaya başlayabilir.
- İçeride fırlayan bir istisna yakalanamaz ve **process'i tamamen çökertir**. Docker
  ortamında ilk açılışta yaşanan `exit code 139` sorununun kökü budur
  ([`DOCKER-REHBER.md`](../DOCKER-REHBER.md) bölüm 5).

**Çözüm:** metodu `static async Task` yapıp `Program.cs` içinde `await` etmek ve gövdeyi
`try/catch` ile sarmalamak.

---

## 🟡 Otomatik migration yok

Uygulama açılırken `Database.Migrate()` çağrılmaz. Boş bir veritabanına karşı çalıştırılırsa
`SeedDatabase` olmayan tablolara sorgu atar ve yukarıdaki çökme yaşanır. Bu yüzden
migration'ların **elle** uygulanması gerekir (bkz. [07 — Kurulum](07-kurulum-ve-calistirma.md)).

---

## 🟡 Iyzipay isteğindeki sabit veriler

`ProcessPayment` içinde aşağıdaki alanlar gerçek veriyle değil sabit değerlerle doldurulur:

| Alan | Sabit değer |
|---|---|
| `Buyer.Surname` | `"Doe"` |
| `Buyer.Email` | sabit bir hotmail adresi |
| `Buyer.Id` | `"BY789"` |
| `Buyer.IdentityNumber` | sabit TC no |
| `Buyer.Ip` | sabit IP |
| `Buyer.LastLoginDate` / `RegistrationDate` | 2015 / 2013 tarihleri |
| `Buyer.RegistrationAddress` | sabit adres |
| `request.BasketId` | `"B67832"` — her siparişte aynı |
| `BasketItem.Category1` | her ürün için `"Telefon"` |

Ayrıca `options.BaseUrl` sandbox adresi olarak kodda sabittir. Sandbox testinde sorun
çıkarmaz, ancak canlıya geçişte hem `BaseUrl` konfigürasyona taşınmalı hem de bu alanlar
gerçek kullanıcı/sipariş verisiyle doldurulmalıdır.

---

## 🟡 Kart alanlarında doğrulama yok

`OrderCreateModel` içindeki adres alanları ayrıntılı data annotation'larla korunurken
`CartName`, `CartNumber`, `CartExpirationMonth`, `CartExpirationYear`, `CartCVV` alanlarında
hiçbir attribute yoktur. Boş veya anlamsız girdi doğrudan ödeme sağlayıcısına gider ve hata
ancak Iyzipay'den dönen mesajla anlaşılır. En azından `[Required]` ve uzunluk/format
kuralları eklenmelidir.

---

## 🟡 Kilitleme eşiği 50

```csharp
options.Lockout.MaxFailedAccessAttempts = 50; //normally 5
```

Geliştirme kolaylığı için yükseltilmiş; yorum satırı da bunu söylüyor. Bu hâliyle kaba
kuvvet denemelerine karşı pratikte koruma sağlamaz. Canlıya çıkmadan 5'e (veya makul bir
değere) çekilmelidir. Aynı şekilde parola politikası da gevşetilmiştir — rakam, büyük harf
ve özel karakter zorunluluğu kapalıdır.

---

## 🟡 Seed kullanıcı parolaları sabit ve zayıf

`SeedDatabase` içinde iki kullanıcı `"12345678"` parolasıyla oluşturulur ve bunlardan biri
`Admin` rolündedir. Değerler kaynak kodda açıkça durur. Gerçek bir ortamda bu, doğrudan
yönetici erişimi demektir. Parolanın konfigürasyondan veya ortam değişkeninden okunması ve
ilk girişte değiştirilmeye zorlanması gerekir.

---

## ⚪ Admin paneli tamamen statik

`AdminController.Index` view'a hiçbir veri göndermez. `_AdminCards.cshtml` içindeki
satış/sipariş/ürün sayıları ve `_NewOrders.cshtml` içindeki "Son Siparişler" tablosu
elle yazılmış örnek verilerdir. Panel gerçek verileri göstermiyor.

---

## ⚪ Testler boş

`Tests/CartTests.cs` içindeki iki `[Fact]` metodunun gövdesi yalnızca yorumdan oluşur:

```csharp
[Fact]
public void AddItem_YeniUrun_SepeteEkler()
{
    // Arrange: bos bir Cart olustur ...
    // Act: cart.AddItem(urun, 2) cagir
    // Assert: cart.CartItems tek eleman icermeli ...
}
```

Assert içermedikleri için her koşulda geçerler; `dotnet test` yeşil dönse de hiçbir şey
doğrulanmamaktadır. `Cart.AddItem`, `Cart.DeleteItem`, `AraToplam` ve `Toplam` metodları
saf (bağımlılıksız) olduğu için test yazmaya en uygun yerlerdir — altyapı da hazır.

---

## ⚪ `store.db` hâlâ depoda

Proje SQLite'tan SQL Server'a geçmiş; `dotnet-store.csproj` içindeki
`Microsoft.EntityFrameworkCore.Sqlite` referansı yorum satırına alınmış durumda. Ancak
147 KB'lık `store.db` dosyası hâlâ git tarafından izleniyor ve artık hiçbir yerde
kullanılmıyor. Silinip `.gitignore` dosyasına eklenebilir.

---

## ⚪ Eskimiş yorum satırı

`Program.cs` içinde veritabanı kaydının üstündeki yorumda "Bu proje için Sqlite kullanılıyor"
yazıyor, oysa hemen altındaki satır `options.UseSqlServer(connectionString)` çağırıyor.

---

## ⚪ `AccountCreatModel` yazım hatası

Sınıf adı `AccountCreatModel`, dosya adı ise `AccountCreateModel.cs`. İşlevsel bir etkisi
yok ama isim tutarsızlığı yaratıyor.

---

## Mimari Gözlemler (hata değil, bilinçli tercih)

Aşağıdakiler "düzeltilmesi gereken" şeyler değil; projenin ölçeği büyürse gündeme
gelebilecek başlıklardır.

- **Repository katmanı yok.** Controller'lar `DataContext` üzerine doğrudan sorgu yazar. Bu
  ölçekte fazladan bir soyutlama katmanı genelde faydadan çok gürültü yaratır; sepet gibi
  mantık içeren kısımlar zaten servise taşınmış durumda.
- **KDV oranı gömülü.** `Cart.Toplam()` ve `Order.Toplam()` içinde `1.2` sabiti duruyor.
  Oran değişirse iki ayrı yerde düzeltmek gerekir.
- **Arama `ToLower().Contains()` ile yapılıyor.** SQL tarafında index kullanımını engeller.
  Ürün sayısı büyürse `EF.Functions.Like` veya full-text search düşünülmelidir.
- **`TransferCartToUser` cookie yoksa da çalışır** ama bu durumda gereksiz yere yeni bir
  misafir sepeti ve cookie üretip hemen ardından siler. İşlevsel bir hata değil, fazladan iş.
- **Sipariş durumu kavramı yok.** `Order` entity'sinde durum alanı bulunmadığı için
  "hazırlanıyor / kargoda / teslim edildi" gibi bir akış modellenemiyor (admin panelindeki
  "Onay Bekliyor" rozetlerinin statik olmasının sebebi de bu).

---

## Öncelik Sırası Önerisi

Canlıya çıkma hedefi varsa makul bir sıra:

1. `/Home/Error` action'ını ekle
2. Tutar tutarsızlığını gider (`Toplam()` / `AraToplam()`)
3. `SeedDatabase` metodunu `async Task` yap + otomatik migration ekle
4. Seed parolalarını ve kilitleme/parola politikasını sıkılaştır
5. Iyzipay sabit verilerini ve `BaseUrl` değerini konfigürasyona taşı
6. Kart alanlarına doğrulama ekle
7. Admin panelini gerçek veriyle besle
8. `Cart` metodları için testleri doldur
