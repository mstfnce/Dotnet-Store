using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Models;

// EF Core veritabanı bağlamı (DbContext). Uygulamadaki tüm tablolara buradan erişilir.
// IdentityDbContext'ten türediği için Identity'nin kullanıcı/rol tabloları da otomatik burada yer alır.
// OnModelCreating içinde: uygulama ilk migration'da örnek Slider/Kategori/Urun verileri (seed data) tanımlanıyor.
public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Kategori> Kategoriler { get; set; }
    public DbSet<Slider> Sliderlar { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Slider>().HasData(
            new List<Slider> {
                new Slider { Id=1, Baslik="Slider 1 Başlık", Aciklama="Slider 1 Açıklama", Resim="slider-1.jpeg", Aktif=true, Index=0},
                new Slider { Id=2, Baslik="Slider 2 Başlık", Aciklama="Slider 2 Açıklama", Resim="slider-2.jpeg", Aktif=true, Index=1},
                new Slider { Id=3, Baslik="Slider 3 Başlık", Aciklama="Slider 3 Açıklama", Resim="slider-3.jpeg", Aktif=true, Index=2}
            }
        );

        modelBuilder.Entity<Kategori>().HasData(
            new List<Kategori> {
                new Kategori {Id=1,  KategoriAdi="Telefon",              Url="telefon"},
                new Kategori {Id=2,  KategoriAdi="Bilgisayar",           Url="bilgisayar"},
                new Kategori {Id=3,  KategoriAdi="Tablet",               Url="tablet"},
                new Kategori {Id=4,  KategoriAdi="Erkek Giyim",          Url="erkek-giyim"},
                new Kategori {Id=5,  KategoriAdi="Kadın Giyim",          Url="kadin-giyim"},
                new Kategori {Id=6,  KategoriAdi="Ayakkabı",             Url="ayakkabi"},
                new Kategori {Id=7,  KategoriAdi="Saat",                 Url="saat"},
                new Kategori {Id=8,  KategoriAdi="Parfüm & Kozmetik",    Url="parfum-kozmetik"},
                new Kategori {Id=9,  KategoriAdi="Aksesuar",             Url="aksesuar"},
                new Kategori {Id=10, KategoriAdi="Ev & Mobilya",         Url="ev-mobilya"},
            }
        );

        modelBuilder.Entity<Urun>().HasData(
            new List<Urun>()
            {
                // 1 - Telefon
                new Urun { Id=1,  UrunAdi="iPhone 13 Pro",           Fiyat=36300, Resim="iphone-13-pro.webp",         Aktif=true, Anasayfa=true,  KategoriId=1, Aciklama="Apple iPhone 13 Pro, A15 Bionic çip, ProMotion 120Hz Super Retina XDR ekran ve üçlü kamera sistemi." },
                new Urun { Id=2,  UrunAdi="iPhone X",                Fiyat=29700, Resim="iphone-x.webp",              Aktif=true, Anasayfa=true,  KategoriId=1, Aciklama="Apple iPhone X, OLED Super Retina ekran, Face ID ve çift arka kamera ile klasik bir model." },
                new Urun { Id=3,  UrunAdi="Samsung Galaxy S10",      Fiyat=23100, Resim="samsung-galaxy-s10.webp",    Aktif=true, Anasayfa=true,  KategoriId=1, Aciklama="Samsung Galaxy S10, Dynamic AMOLED ekran, üçlü kamera ve kablosuz ters şarj özelliği." },
                new Urun { Id=4,  UrunAdi="Samsung Galaxy S8",       Fiyat=16500, Resim="samsung-galaxy-s8.webp",     Aktif=true, Anasayfa=false, KategoriId=1, Aciklama="Samsung Galaxy S8, kavisli Infinity Display ve IP68 su geçirmezlik standardı." },
                new Urun { Id=5,  UrunAdi="Oppo F19 Pro Plus",       Fiyat=13200, Resim="oppo-f19-pro-plus.webp",     Aktif=true, Anasayfa=false, KategoriId=1, Aciklama="Oppo F19 Pro Plus 5G, AMOLED ekran ve 65W SuperVOOC 2.0 hızlı şarj." },

                // 2 - Bilgisayar
                new Urun { Id=6,  UrunAdi="MacBook Pro 14\" Space Grey", Fiyat=66000, Resim="macbook-pro-14.webp",    Aktif=true, Anasayfa=true,  KategoriId=2, Aciklama="Apple MacBook Pro 14 inç, M serisi çip, Liquid Retina XDR ekran ve uzun pil ömrü." },
                new Urun { Id=7,  UrunAdi="Asus Zenbook Pro Duo",    Fiyat=59400, Resim="asus-zenbook-pro.webp",      Aktif=true, Anasayfa=true,  KategoriId=2, Aciklama="Asus Zenbook Pro çift ekranlı dizüstü, OLED ekran ve içerik üreticileri için tasarım." },
                new Urun { Id=8,  UrunAdi="Huawei MateBook X Pro",   Fiyat=46200, Resim="huawei-matebook-x-pro.webp", Aktif=true, Anasayfa=false, KategoriId=2, Aciklama="Huawei MateBook X Pro, 3K dokunmatik ekran, ince magnezyum kasa ve uzun pil ömrü." },
                new Urun { Id=9,  UrunAdi="Dell XPS 13 9300",        Fiyat=49500, Resim="dell-xps-13.webp",           Aktif=true, Anasayfa=false, KategoriId=2, Aciklama="Dell XPS 13 9300, InfinityEdge 4K ekran ve Intel Core i7 işlemci." },
                new Urun { Id=10, UrunAdi="Lenovo Yoga 920",         Fiyat=36300, Resim="lenovo-yoga-920.webp",       Aktif=true, Anasayfa=false, KategoriId=2, Aciklama="Lenovo Yoga 920 2-in-1 dönüştürülebilir dizüstü, dokunmatik ekran ve Active Pen desteği." },

                // 3 - Tablet
                new Urun { Id=11, UrunAdi="iPad Mini 2021 Starlight", Fiyat=16500, Resim="ipad-mini-2021.webp",       Aktif=true, Anasayfa=true,  KategoriId=3, Aciklama="Apple iPad Mini 2021, A15 Bionic çip, 8.3 inç Liquid Retina ekran ve Apple Pencil 2 desteği." },
                new Urun { Id=12, UrunAdi="Galaxy Tab S8+ Grey",     Fiyat=19800, Resim="galaxy-tab-s8-plus.webp",    Aktif=true, Anasayfa=true,  KategoriId=3, Aciklama="Samsung Galaxy Tab S8+, 12.4 inç Super AMOLED ekran ve S Pen ile profesyonel kullanım." },
                new Urun { Id=13, UrunAdi="Galaxy Tab",              Fiyat=11500, Resim="galaxy-tab-white.webp",      Aktif=true, Anasayfa=false, KategoriId=3, Aciklama="Samsung Galaxy Tab, geniş ekran ve uzun pil ömrü ile günlük kullanım için ideal tablet." },

                // 4 - Erkek Giyim
                new Urun { Id=14, UrunAdi="Mavi & Siyah Kareli Gömlek", Fiyat=990, Resim="mavi-siyah-kareli-gomlek.webp", Aktif=true, Anasayfa=false, KategoriId=4, Aciklama="Pamuklu, regular fit mavi ve siyah kareli erkek gömlek — günlük şıklık." },
                new Urun { Id=15, UrunAdi="Ekoseli Erkek Gömlek",    Fiyat=1155, Resim="ekoseli-gomlek.webp",          Aktif=true, Anasayfa=false, KategoriId=4, Aciklama="Klasik ekose desenli uzun kollu erkek gömlek, slim fit kalıp." },
                new Urun { Id=16, UrunAdi="Kısa Kollu Erkek Gömlek", Fiyat=660,  Resim="kisa-kollu-gomlek.webp",       Aktif=true, Anasayfa=false, KategoriId=4, Aciklama="Yaz aylarında konforlu kullanım için pamuklu kısa kollu erkek gömlek." },
                new Urun { Id=17, UrunAdi="Kareli Erkek Gömlek",     Fiyat=924,  Resim="kareli-erkek-gomlek.webp",     Aktif=true, Anasayfa=false, KategoriId=4, Aciklama="Klasik kareli desen, regular fit erkek gömlek — ofis ve sosyal etkinlikler için." },
                new Urun { Id=18, UrunAdi="Aorus Gaming Tişört",     Fiyat=825,  Resim="aorus-tisort.webp",            Aktif=true, Anasayfa=false, KategoriId=4, Aciklama="Gigabyte Aorus baskılı pamuklu gaming tişört, oyun severler için." },

                // 5 - Kadın Giyim
                new Urun { Id=19, UrunAdi="Siyah Uzun Gece Elbisesi", Fiyat=4290, Resim="siyah-gece-elbisesi.webp",    Aktif=true, Anasayfa=true,  KategoriId=5, Aciklama="Şık tasarımlı siyah uzun gece elbisesi — özel davetler ve gece etkinlikleri için." },
                new Urun { Id=20, UrunAdi="Deri Korse ve Etek Takım", Fiyat=2970, Resim="deri-korse-etek.webp",        Aktif=true, Anasayfa=true,  KategoriId=5, Aciklama="Deri korse ve uyumlu etek takımı — modern ve iddialı bir görünüm." },
                new Urun { Id=21, UrunAdi="Marni Kırmızı & Siyah Takım", Fiyat=5940, Resim="marni-takim.webp",         Aktif=true, Anasayfa=false, KategoriId=5, Aciklama="Marni tasarım kırmızı ve siyah iki parça takım, premium kumaş ve özel kesim." },
                new Urun { Id=22, UrunAdi="Pea Elbise",              Fiyat=1650, Resim="pea-elbise.webp",              Aktif=true, Anasayfa=false, KategoriId=5, Aciklama="Hafif ve şık günlük elbise, sade tasarımıyla her ortama uygun." },
                new Urun { Id=23, UrunAdi="Siyah Korse ve Etek",     Fiyat=2640, Resim="siyah-korse-etek.webp",        Aktif=true, Anasayfa=false, KategoriId=5, Aciklama="Klasik siyah korse ve etek kombini — zarif ve feminen bir tarz." },

                // 6 - Ayakkabı
                new Urun { Id=24, UrunAdi="Nike Air Jordan 1 Red & Black", Fiyat=4950, Resim="nike-air-jordan-1.webp", Aktif=true, Anasayfa=true,  KategoriId=6, Aciklama="Nike Air Jordan 1 ikonik kırmızı-siyah renk kombinasyonu, klasik basketbol siluet." },
                new Urun { Id=25, UrunAdi="Puma Future Rider",       Fiyat=2970, Resim="puma-future-rider.webp",       Aktif=true, Anasayfa=true,  KategoriId=6, Aciklama="Puma Future Rider retro tasarım spor ayakkabı, hafif ve günlük konfor." },
                new Urun { Id=26, UrunAdi="Spor Ayakkabı Beyaz & Kırmızı", Fiyat=3960, Resim="spor-ayakkabi-beyaz-kirmizi.webp", Aktif=true, Anasayfa=false, KategoriId=6, Aciklama="Off-white ve kırmızı renkli sneaker, modern siluet ve dayanıklı taban." },
                new Urun { Id=27, UrunAdi="Nike Baseball Cleats",    Fiyat=2640, Resim="nike-baseball-cleats.webp",    Aktif=true, Anasayfa=false, KategoriId=6, Aciklama="Nike beyzbol ayakkabısı, sahada maksimum tutuş ve hareket kabiliyeti." },
                new Urun { Id=28, UrunAdi="Sports Sneakers Off White", Fiyat=3630, Resim="spor-ayakkabi-off-white.webp", Aktif=true, Anasayfa=false, KategoriId=6, Aciklama="Off-white tonlarda spor ayakkabı, sokak modasına uygun şık tasarım." },

                // 7 - Saat
                new Urun { Id=29, UrunAdi="Longines Master Collection", Fiyat=49500, Resim="longines-master.webp",    Aktif=true, Anasayfa=true,  KategoriId=7, Aciklama="Longines Master Collection, İsviçre otomatik mekanizma ve klasik tasarım." },
                new Urun { Id=30, UrunAdi="Rolex Cellini Date Black",  Fiyat=297000, Resim="rolex-cellini-date.webp",  Aktif=true, Anasayfa=true,  KategoriId=7, Aciklama="Rolex Cellini Date, siyah kadran, otomatik mekanizma ve 18 ayar altın kasa." },
                new Urun { Id=31, UrunAdi="Rolex Cellini Moonphase",   Fiyat=429000, Resim="rolex-cellini-moonphase.webp", Aktif=true, Anasayfa=false, KategoriId=7, Aciklama="Rolex Cellini Moonphase, ay safhası göstergesi ve premium İsviçre işçiliği." },
                new Urun { Id=32, UrunAdi="Rolex Datejust",            Fiyat=363000, Resim="rolex-datejust.webp",      Aktif=true, Anasayfa=false, KategoriId=7, Aciklama="Rolex Datejust, ikonik tasarım, otomatik mekanizma ve sürekli güncellenen tarih göstergesi." },
                new Urun { Id=33, UrunAdi="Kahverengi Deri Kayışlı Saat", Fiyat=2970, Resim="kahverengi-deri-kayisli-saat.webp", Aktif=true, Anasayfa=false, KategoriId=7, Aciklama="Klasik tasarımlı kahverengi deri kayışlı kol saati, günlük şık kullanım." },

                // 8 - Parfüm & Kozmetik
                new Urun { Id=34, UrunAdi="Chanel Coco Noir EDP",    Fiyat=4290, Resim="chanel-coco-noir.webp",        Aktif=true, Anasayfa=true,  KategoriId=8, Aciklama="Chanel Coco Noir Eau de Parfum, gizemli ve oryantal kadın parfümü." },
                new Urun { Id=35, UrunAdi="Dior J'adore EDP",        Fiyat=2970, Resim="dior-jadore.webp",             Aktif=true, Anasayfa=true,  KategoriId=8, Aciklama="Dior J'adore, çiçeksi ve feminen — klasik bir kadın parfümü." },
                new Urun { Id=36, UrunAdi="Gucci Bloom EDP",         Fiyat=2640, Resim="gucci-bloom.webp",             Aktif=true, Anasayfa=false, KategoriId=8, Aciklama="Gucci Bloom, beyaz çiçek kompozisyonu ile zarif kadın parfümü." },
                new Urun { Id=37, UrunAdi="Essence Lash Princess Maskara", Fiyat=330, Resim="essence-mascara.webp",    Aktif=true, Anasayfa=false, KategoriId=8, Aciklama="Essence Lash Princess False Lash Effect maskara — yoğun ve uzun kirpik etkisi." },
                new Urun { Id=38, UrunAdi="Kırmızı Mat Ruj",         Fiyat=429,  Resim="kirmizi-ruj.webp",             Aktif=true, Anasayfa=false, KategoriId=8, Aciklama="Mat bitişli, uzun süre kalıcı klasik kırmızı ruj." },

                // 9 - Aksesuar
                new Urun { Id=39, UrunAdi="Prada Kadın El Çantası",  Fiyat=19800, Resim="prada-canta.webp",            Aktif=true, Anasayfa=true,  KategoriId=9, Aciklama="Prada deri kadın el çantası — lüks ve zarif tasarım." },
                new Urun { Id=40, UrunAdi="Heshe Deri Kadın Çantası", Fiyat=4290, Resim="heshe-deri-canta.webp",       Aktif=true, Anasayfa=false, KategoriId=9, Aciklama="Heshe markası gerçek deri kadın çanta, geniş hacim ve klasik tasarım." },
                new Urun { Id=41, UrunAdi="Mavi Kadın El Çantası",   Fiyat=1650, Resim="mavi-el-cantasi.webp",         Aktif=true, Anasayfa=false, KategoriId=9, Aciklama="Mavi renk şık kadın el çantası — günlük ve özel etkinlikler için." },
                new Urun { Id=42, UrunAdi="Siyah Güneş Gözlüğü",     Fiyat=990,  Resim="siyah-gunes-gozlugu.webp",     Aktif=true, Anasayfa=false, KategoriId=9, Aciklama="Klasik siyah çerçeveli güneş gözlüğü, UV400 koruma." },
                new Urun { Id=43, UrunAdi="Yeşil & Siyah Güneş Gözlüğü", Fiyat=1155, Resim="yesil-siyah-gozluk.webp",  Aktif=true, Anasayfa=false, KategoriId=9, Aciklama="Yeşil ve siyah ton kombinli moda güneş gözlüğü — günlük şık aksesuar." },

                // 10 - Ev & Mobilya
                new Urun { Id=44, UrunAdi="Annibale Colombo Yatak",   Fiyat=62700, Resim="annibale-colombo-yatak.webp", Aktif=true, Anasayfa=true,  KategoriId=10, Aciklama="Annibale Colombo el yapımı çift kişilik yatak — İtalyan tasarımı ve premium malzeme." },
                new Urun { Id=45, UrunAdi="Annibale Colombo Koltuk", Fiyat=82500, Resim="annibale-colombo-koltuk.webp", Aktif=true, Anasayfa=true,  KategoriId=10, Aciklama="Annibale Colombo lüks oturma grubu koltuk, hakiki deri kaplama." },
                new Urun { Id=46, UrunAdi="Knoll Saarinen Sandalye",  Fiyat=16500, Resim="knoll-saarinen-sandalye.webp", Aktif=true, Anasayfa=false, KategoriId=10, Aciklama="Knoll Saarinen Executive konferans sandalyesi, ikonik orta yüzyıl modern tasarım." },
                new Urun { Id=47, UrunAdi="Komodin African Cherry",  Fiyat=9900,  Resim="komodin-african-cherry.webp", Aktif=true, Anasayfa=false, KategoriId=10, Aciklama="African Cherry ahşap komodin, sade ve sıcak detaylarla yatak odasına şıklık." },
                new Urun { Id=48, UrunAdi="Modern Masa Lambası",     Fiyat=1650,  Resim="masa-lambasi.webp",           Aktif=true, Anasayfa=false, KategoriId=10, Aciklama="Modern tasarım masa lambası — çalışma masası ve okuma köşeleri için ideal aydınlatma." },
            }
        );
    }

}
