# Dotnet Store — Teknik Dokümantasyon

Bu klasör, projenin nasıl kurulduğunu, hangi parçalardan oluştuğunu ve bu parçaların
birbiriyle nasıl konuştuğunu anlatır. Kod okumadan önce buradan başlanabilir.

## İçindekiler

| # | Doküman | Ne anlatıyor |
|---|---|---|
| 01 | [Mimari Genel Bakış](01-mimari.md) | Katmanlar, istek yaşam döngüsü, DI kayıtları, middleware pipeline, routing |
| 02 | [Veri Modeli](02-veri-modeli.md) | Entity'ler, ilişkiler, ER diyagramı, migration'lar, seed verisi |
| 03 | [Controller ve Rota Referansı](03-controller-ve-rotalar.md) | Tüm endpoint'ler, HTTP metodları ve yetki gereksinimleri |
| 04 | [Servisler ve View Component'ler](04-servisler.md) | CartService, ImageService, SmtpEmailService, Navbar/Slider |
| 05 | [Kimlik Doğrulama ve Yetkilendirme](05-kimlik-dogrulama.md) | Identity yapılandırması, roller, parola politikası, hesap akışları |
| 06 | [Sepet, Sipariş ve Ödeme Akışı](06-siparis-ve-odeme.md) | Misafir sepetten ödeme onayına kadar uçtan uca akış |
| 07 | [Kurulum ve Çalıştırma](07-kurulum-ve-calistirma.md) | Yerel kurulum, konfigürasyon, migration, Docker, testler |
| 08 | [Geliştirme Notları](08-gelistirme-notlari.md) | Bilinen eksikler, teknik borç, dikkat edilmesi gerekenler |

## Hızlı Özet

**Dotnet Store**, ASP.NET Core MVC ile yazılmış bir e-ticaret uygulamasıdır.
Ziyaretçi tarafında kategori/ürün listeleme, arama, sepet ve ödeme; yönetici tarafında
ürün, kategori, slider, kullanıcı, rol ve sipariş yönetimi vardır.

| | |
|---|---|
| **Framework** | .NET 9 / ASP.NET Core MVC (Razor Views) |
| **Veritabanı** | Microsoft SQL Server + Entity Framework Core 9 |
| **Kimlik** | ASP.NET Core Identity (`int` anahtarlı, cookie tabanlı) |
| **Ödeme** | Iyzipay (sandbox) |
| **Görsel işleme** | SkiaSharp (yeniden boyutlama + WebP dönüşümü) |
| **E-posta** | SMTP (`System.Net.Mail`) |
| **Test** | xUnit (`Tests/` projesi) |
| **Dağıtım** | Dockerfile (multi-stage) + docker-compose (app + SQL Server) |

## Diğer Dokümanlar

- [`../README.md`](../README.md) — projenin İngilizce tanıtım ve hızlı başlangıç dosyası
- [`../DOCKER-REHBER.md`](../DOCKER-REHBER.md) — Docker kavramları ve sorun giderme rehberi (Türkçe)
