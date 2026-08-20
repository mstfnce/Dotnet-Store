# 03 — Controller ve Rota Referansı

Aşağıdaki tablolar uygulamadaki tüm endpoint'leri listeler. Yetki sütunu şu anlamlara gelir:

| İşaret | Anlam |
|---|---|
| 🌐 | Herkese açık, giriş gerekmez |
| 🔒 | Giriş yapmış olmak yeterli (`[Authorize]`) |
| 👑 | Yalnızca `Admin` rolü (`[Authorize(Roles = "Admin")]`) |

> **Async son eki:** `ChangePasswordAsync`, `DeleteAsync` gibi action adlarındaki `Async`
> soneki ASP.NET Core tarafından otomatik olarak kaldırılır. URL'de `/Account/ChangePassword`
> ve `/User/Delete` şeklinde görünürler.

---

## HomeController 🌐

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/` veya `/Home/Index` | Anasayfa. `Aktif && Anasayfa` olan ürünleri ve tüm kategorileri listeler |

---

## UrunController — Ürünler

Sınıf düzeyinde 👑 işaretlidir; müşteriye açık iki action `[AllowAnonymous]` ile bu kuraldan
muaf tutulmuştur.

| Metot | Rota | Yetki | Açıklama |
|---|---|---|---|
| GET | `/Urun/Index?kategori={id}` | 👑 | Yönetim listesi, isteğe bağlı kategori filtresi |
| GET | `/urunler/{url?}?q={arama}` | 🌐 | Müşteri listesi — kategori ve/veya isim araması |
| GET | `/Urun/Details/{id}` | 🌐 | Ürün detayı + aynı kategoriden 4 benzer ürün |
| GET / POST | `/Urun/Create` | 👑 | Yeni ürün (görsel yükleme dahil) |
| GET / POST | `/Urun/Edit/{id}` | 👑 | Ürün düzenleme |
| GET | `/Urun/Delete/{id}` | 👑 | Silme onay sayfası |
| POST | `/Urun/DeleteConfirm/{id}` | 👑 | Silmeyi gerçekleştirir |

Arama büyük/küçük harf duyarsızdır (iki taraf da `ToLower()` ile karşılaştırılır) ve yalnızca
ürün adında arar. Listede yalnızca `Aktif` ürünler döner.

---

## KategoriController — Kategoriler 👑

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/Kategori/Index` | Kategori listesi (ürün sayısıyla birlikte) |
| GET / POST | `/Kategori/Create` | Yeni kategori |
| GET / POST | `/Kategori/Edit/{id}` | Kategori düzenleme |
| GET | `/Kategori/Delete/{id}` | Silme onay sayfası |
| POST | `/Kategori/DeleteConfirm/{id}` | Silmeyi gerçekleştirir |

---

## SliderController — Anasayfa Banner'ları 👑

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/Slider/Index` | Slider listesi |
| GET / POST | `/Slider/Create` | Yeni slider (görsel yükleme) |
| GET / POST | `/Slider/Edit/{id}` | Slider düzenleme |
| GET | `/Slider/Delete/{id}` | Silme onay sayfası |
| POST | `/Slider/DeleteConfirm/{id}` | Silmeyi gerçekleştirir |

---

## CartController — Sepet 🌐

Sepet giriş gerektirmez; misafir kullanıcının sepeti cookie ile takip edilir.

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/Cart/Index` | Sepet sayfası |
| POST | `/Cart/AddToCart` | `urunId`, `miktar` (varsayılan 1) — ekledikten sonra sepete yönlendirir |
| POST | `/Cart/RemoveItem` | `urunId`, `miktar` — azaltır, sıfırlanırsa satırı siler |

---

## OrderController — Siparişler

Sınıf düzeyinde 🔒'dir; yönetim action'ları ayrıca 👑 ister.

| Metot | Rota | Yetki | Açıklama |
|---|---|---|---|
| GET | `/Order/Index` | 👑 | Tüm siparişlerin listesi |
| GET | `/Order/Details/{id}` | 👑 | Sipariş detayı (satırlar + ürünler) |
| GET | `/Order/Checkout` | 🔒 | Ödeme formu (adres + kart bilgisi) |
| POST | `/Order/Checkout` | 🔒 | Ödemeyi alır, başarılıysa siparişi kaydeder ve sepeti siler |
| GET | `/Order/Completed?orderId={id}` | 🔒 | Sipariş tamamlandı sayfası |
| GET | `/Order/OrderList` | 🔒 | Kullanıcının kendi sipariş geçmişi |

Detaylı akış → [06 — Sepet, Sipariş ve Ödeme Akışı](06-siparis-ve-odeme.md).

---

## AccountController — Hesap İşlemleri

| Metot | Rota | Yetki | Açıklama |
|---|---|---|---|
| GET / POST | `/Account/Create` | 🌐 | Kayıt ol |
| GET / POST | `/Account/Login` | 🌐 | Giriş yap (`returnUrl` desteklenir) |
| GET | `/Account/LogOut` | 🔒 | Çıkış |
| GET | `/Account/Settings` | 🔒 | Hesap ayarları ana sayfası |
| GET / POST | `/Account/EditUser` | 🔒 | Ad soyad ve e-posta güncelleme |
| GET / POST | `/Account/ChangePassword` | 🔒 | Parola değiştirme (mevcut parola doğrulanır) |
| GET / POST | `/Account/ForgotPassword` | 🌐 | Sıfırlama bağlantısını e-posta ile gönderir |
| GET / POST | `/Account/ResetPassword` | 🌐 | Token ile yeni parola belirleme |
| GET | `/Account/AccessDenied` | 🌐 | Yetkisiz erişim sayfası |

Giriş başarılı olduğunda misafir sepeti kullanıcı hesabına aktarılır
(`ICartService.TransferCartToUser`).

---

## UserController — Kullanıcı Yönetimi 👑

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/User/Index?role={rol}` | Kullanıcı listesi, isteğe bağlı rol filtresi |
| GET / POST | `/User/Create` | Yeni kullanıcı |
| GET / POST | `/User/Edit/{id}` | Kullanıcı bilgileri ve rol atamaları |
| GET | `/User/Delete/{id}` | Silme onay sayfası |
| POST | `/User/DeleteConfirm/{id}` | Silmeyi gerçekleştirir |

---

## RoleController — Rol Yönetimi 👑

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/Role/Index` | Rol listesi |
| GET / POST | `/Role/Create` | Yeni rol |
| GET / POST | `/Role/Edit/{id}` | Rol adı ve rolün kullanıcıları |
| GET | `/Role/Delete/{id}` | Silme onay sayfası |
| POST | `/Role/DeleteConfirm/{id}` | Silmeyi gerçekleştirir |

---

## AdminController — Yönetim Paneli 👑

| Metot | Rota | Açıklama |
|---|---|---|
| GET | `/Admin/Index` | Panel anasayfası — `_AdminCards` ve `_NewOrders` partial'larını render eder |

> Panelin her iki bölümü de şu an **statik**tir. Özet kartlardaki sayılar
> (`Satış`, `Sipariş`, `Ürün` ...) `_AdminCards.cshtml` içinde, "Son Siparişler"
> tablosundaki satırlar da `_NewOrders.cshtml` içinde sabit yazılıdır. `AdminController.Index`
> view'a hiçbir veri göndermez. Bkz. [08 — Geliştirme Notları](08-gelistirme-notlari.md).

---

## Form Model'leri

Controller'lar entity'leri doğrudan bağlamak yerine `Models/` altındaki form model'lerini
kullanır. Doğrulama kuralları (`[Required]`, `[StringLength]`, `[EmailAddress]`,
`[Compare]`, `[RegularExpression]`) bu sınıflardaki data annotation'larla tanımlıdır ve
Türkçe hata mesajları içerir.

| Klasör | Sınıflar |
|---|---|
| `Models/Account/` | `AccountCreatModel`, `AccountLoginModel`, `AccountEditUserModel`, `AccountChangePasswordModel`, `AccountResetPasswordModel` |
| `Models/Kategori/` | `KategoriCreateModel`, `KategoriEditModel`, `KategoriGetModel` |
| `Models/Urun/` | `UrunCreateModel`, `UrunEditModel`, `UrunGetModel`, `UrunModel` |
| `Models/Slider/` | `SliderCreateModel`, `SliderEditModel`, `SliderGetModel` |
| `Models/Order/` | `OrderCreateModel` |
| `Models/User/` | `UserCreateModel`, `UserEditModel` |
| `Models/Role/` | `RoleCreateModel`, `RoleEditModel` |

`...GetModel` sınıfları listeleme içindir (örn. `KategoriGetModel.UrunSayisi` gibi
hesaplanmış alanlar taşır); `...CreateModel` / `...EditModel` sınıfları form gönderimi içindir.
