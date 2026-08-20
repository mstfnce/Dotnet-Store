# 07 — Kurulum ve Çalıştırma

## Gereksinimler

| Araç | Not |
|---|---|
| [.NET 9 SDK](https://dotnet.microsoft.com/download) | Zorunlu |
| Microsoft SQL Server | LocalDB, SQL Express, tam sürüm veya Docker container |
| `dotnet-ef` | `dotnet tool install --global dotnet-ef` |
| Docker Desktop | Yalnızca container ile çalıştıracaksan |

## Yerel Kurulum

```bash
git clone https://github.com/mstfnce/Dotnet-Store.git
cd Dotnet-Store

dotnet restore
dotnet ef database update
dotnet run
```

`dotnet run` çıktısında yazan adresten uygulamaya erişilir. Tanımlı profiller
(`Properties/launchSettings.json`):

| Profil | Adres(ler) |
|---|---|
| `http` | `http://localhost:5163` |
| `https` | `https://localhost:7059` ve `http://localhost:5162` |

Belirli bir profille çalıştırmak için:

```bash
dotnet run --launch-profile https
```

Her iki profil de `ASPNETCORE_ENVIRONMENT=Development` ile başlar.

## Konfigürasyon

### Veritabanı Bağlantısı

`appsettings.json` (git'te bulunur, varsayılan LocalDB):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\MSSQLLocalDB;Database=DotnetStoreDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Yerel makinede farklı bir sunucu kullanıyorsan `appsettings.Development.json` içinde
üzerine yazabilirsin (örn. `Server=.\sqlexpress`).

### Gizli Ayarlar

`appsettings.Development.json` **`.gitignore` içindedir ve depoya girmez.** Projeyi yeni
klonladıysan bu dosyayı kendin oluşturman gerekir:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\sqlexpress;Database=storeDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Email": {
    "Host": "smtp.example.com",
    "Username": "mail@example.com",
    "Password": "uygulama-parolasi"
  },
  "PaymentApi": {
    "APIKey": "sandbox-...",
    "SecretKey": "sandbox-..."
  }
}
```

| Bölüm | Ne için gerekli | Yoksa ne olur |
|---|---|---|
| `ConnectionStrings` | Veritabanı | Uygulama açılmaz |
| `Email` | Parola sıfırlama e-postası | Yalnızca "şifremi unuttum" akışı çalışmaz |
| `PaymentApi` | Iyzipay sandbox anahtarları | Checkout'ta ödeme başarısız olur |

Iyzipay anahtarları [sandbox-merchant.iyzipay.com](https://sandbox-merchant.iyzipay.com)
üzerinden ücretsiz alınabilir. Yapılandırma anahtarları .NET'te büyük/küçük harf duyarsızdır,
bu yüzden `APIKey` ve `ApiKey` yazımları aynı değere çözülür.

> Gerçek bir sunucuya çıkarken bu değerleri dosya yerine ortam değişkeni olarak vermek
> daha güvenlidir: `ConnectionStrings__DefaultConnection`, `Email__Password` gibi
> (çift alt çizgi iç içe anahtarı temsil eder).

## Veritabanı ve Migration

```bash
# Mevcut migration'ları uygula
dotnet ef database update

# Yeni migration üret
dotnet ef migrations add MigrationAdi

# Son migration'ı geri al (henüz uygulanmadıysa)
dotnet ef migrations remove

# Veritabanını tamamen sıfırla
dotnet ef database drop
dotnet ef database update
```

Migration'lar uygulandığında slider, kategori ve ürün seed verisi de gelir. Rol ve
kullanıcı seed'i ise uygulama ilk açıldığında `SeedDatabase.Initialize` tarafından yapılır.
Varsayılan hesaplar → [05 — Kimlik Doğrulama](05-kimlik-dogrulama.md#seed-kullanıcılar).

## Docker ile Çalıştırma

Proje iki servisli bir compose dosyasıyla gelir: `app` (kendi Dockerfile'ından build edilir)
ve `db` (hazır SQL Server 2022 image'ı).

```bash
docker compose up --build
```

**İlk çalıştırmada** veritabanı boş gelir; migration'ları host makineden container'daki
veritabanına uygulaman gerekir:

```bash
dotnet ef database update --connection "Server=localhost,1433;Database=DotnetStoreDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
```

Ardından uygulamayı yeniden başlat:

```bash
docker compose up -d app
```

| Erişim | Adres |
|---|---|
| Uygulama | http://localhost:8080 |
| Veritabanı (SSMS / Azure Data Studio) | `localhost,1433` — kullanıcı `sa` |

Günlük kullanım, komut listesi ve sık karşılaşılan hatalar için ayrıntılı Türkçe rehber:
[`../DOCKER-REHBER.md`](../DOCKER-REHBER.md).

## Testler

Test projesi `Tests/dotnet-store.Tests.csproj` dosyasındadır ve xUnit kullanır. Ana proje
`.csproj` içinde `Tests/**` derlemeden hariç tutulmuştur; test projesi ana projeye
`ProjectReference` ile bağlanır.

```bash
dotnet test
```

> Şu an `Tests/CartTests.cs` içindeki iki test gövdesi boştur — yalnızca yorum satırı
> hâlinde plan içerirler ve her koşulda geçerler. Bkz. [08 — Geliştirme Notları](08-gelistirme-notlari.md).

## Faydalı Komutlar

```bash
dotnet build                 # Derle
dotnet run                   # Çalıştır
dotnet watch run             # Değişiklikte otomatik yeniden başlat
dotnet test                  # Testleri çalıştır
dotnet clean                 # bin/obj temizliği
```

## Git'e Girmemesi Gerekenler

`.gitignore` tarafından zaten dışlananlar:

- `bin/`, `obj/`
- `build-check/`
- `appsettings.Development.json`
- Yerel veritabanı dosyaları
