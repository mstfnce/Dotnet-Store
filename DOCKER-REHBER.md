# Docker Kullanım Kılavuzu (dotnet-store-4)

## 1. Temel Kavramlar Sözlüğü

| Terim | Anlamı |
|---|---|
| **Image** | Uygulamanın donmuş, çalıştırılabilir paketi. Kurulum paketi gibi düşün — değişmez (immutable). |
| **Container** | Image'ın çalışan hali. Aynı image'dan birden fazla container başlatılabilir. |
| **Dockerfile** | Image'ın nasıl inşa edileceğini adım adım anlatan tarif dosyası. |
| **Layer (katman)** | Dockerfile'daki her satır bir katman oluşturur; Docker değişmeyenleri cache'ler, build'i hızlandırır. |
| **Registry** | Image'ların depolandığı yer (Docker Hub, Microsoft'un `mcr.microsoft.com` registry'si gibi). |
| **Daemon (Docker Engine)** | Arka planda çalışıp asıl build/run işini yapan servis. `docker` komutu ona mesaj gönderen bir istemci. Windows'ta bu, Docker Desktop açıkken çalışır. |
| **Multi-stage build** | Derleme aracının (SDK) kendisini final image'a sokmadan, sadece derleme *sonucunu* taşımak için birden fazla `FROM` aşaması kullanma tekniği. Image küçük ve temiz kalır. |
| **Volume** | Container dışında yaşayan kalıcı disk alanı. Container silinse de volume'deki veri kalır. |
| **Port mapping** (`-p host:container`) | Host makinedeki bir portu, container içindeki bir porta bağlar. Bağlanmazsa dışarıdan container'a erişilemez. |
| **docker-compose** | Birden fazla container'ı (servisi) tek YAML dosyasında tanımlayıp tek komutla birlikte ayağa kaldırma aracı. |
| **Service** | Compose dosyasındaki her madde — bir container'ın tarifi (bizde `app` ve `db`). |
| **Healthcheck** | Docker'a "bu servis gerçekten hazır mı?" diye periyodik kontrol ettirme. `depends_on: condition: service_healthy` ile başka bir servisin bunu beklemesini sağlarız. |
| **Network (iç ağ)** | Compose, aynı dosyadaki servisleri otomatik bir ağ üzerinden birbirine bağlar; servisler birbirine **servis adıyla** (örn. `Server=db`) ulaşır. |

## 2. Bu Projedeki Dockerfile

İki aşamalı (multi-stage) build:
1. **build aşaması** (`sdk:9.0`) — projeyi `dotnet restore` + `dotnet publish` ile derler.
2. **final aşama** (`aspnet:9.0`, sadece runtime) — sadece derlenmiş çıktıyı kopyalar, 8080 portundan yayına başlar.

Detaylar için `Dockerfile` içindeki yorum satırlarına bak.

## 3. Bu Projedeki docker-compose.yml

İki servis:
- **`db`** — hazır `mssql/server` image'ı, verisi `db-data` volume'ünde kalıcı, healthcheck ile hazır olup olmadığı kontrol edilir.
- **`app`** — kendi Dockerfile'ımızdan build edilir, `db` sağlıklı olunca başlar, ona `Server=db` ile bağlanır.

Detaylar için `docker-compose.yml` içindeki yorum satırlarına bak.

## 4. Sık Kullanılan Komutlar

```bash
# Build + tüm servisleri başlat (loglar terminalde akar)
docker compose up --build

# Arka planda (detached) başlat
docker compose up -d --build

# Sadece belirli bir servisi (yeniden) başlat
docker compose up -d app

# Çalışan container'ları listele
docker ps
docker ps -a          # durmuş olanlar dahil

# Logları izle
docker compose logs -f app

# Durdur (volume'ler korunur)
docker compose down

# Durdur + volume'leri de sil (veritabanı verisi dahil sıfırlanır)
docker compose down -v
```

## 5. Karşılaştığımız Sorunlar ve Çözümleri

### "failed to connect to the docker API ... npipe" hatası
**Sebep:** Docker Desktop uygulaması (daemon) açık değildi. `docker` komutu sadece istemci; arkasında çalışan daemon olmadan hiçbir şey yapamaz.
**Çözüm:** Docker Desktop'ı aç, balina ikonu sabitlenene kadar bekle, sonra tekrar dene.

### `app` container'ı çöktü (exit code 139), `SeedDatabase.cs` hatası
**Sebep:** SQL Server container'ı boş başlar; veritabanı tabloları migration'lar çalıştırılmadan oluşmaz. Kod olmayan tablolara sorgu atınca çöktü (`Initialize` metodu `async void` olduğu için hata process'i tamamen çökertti).
**Çözüm:** `db` servisinin portu (1433) host'a açık olduğu için, host'tan migration uyguladık:
```bash
dotnet ef database update --connection "Server=localhost,1433;Database=DotnetStoreDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
```
Sonra `docker compose up -d app` ile uygulamayı yeniden başlattık.

**Not:** Bu, sadece ilk kurulumda elle yapılması gereken bir adım. İstenirse `Program.cs`'e otomatik migration kodu eklenebilir, o zaman bu adım gerekmez.

## 6. Uygulamaya Erişim

- Uygulama: http://localhost:8080
- Veritabanı (SSMS / Azure Data Studio ile): `localhost,1433`, kullanıcı `sa`, parola `YourStrong!Passw0rd` (sadece yerel geliştirme — production'da asla böyle bırakılmaz).
