using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_store.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Bilgisayar", "bilgisayar" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Tablet", "tablet" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Erkek Giyim", "erkek-giyim" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kadın Giyim", "kadin-giyim" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Ayakkabı", "ayakkabi" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Saat", "saat" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Parfüm & Kozmetik", "parfum-kozmetik" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Aksesuar", "aksesuar" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Ev & Mobilya", "ev-mobilya" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Aciklama", "Aktif", "Fiyat", "Resim", "UrunAdi" },
                values: new object[] { "Apple iPhone 13 Pro, A15 Bionic çip, ProMotion 120Hz Super Retina XDR ekran ve üçlü kamera sistemi.", true, 36300.0, "iphone-13-pro.webp", "iPhone 13 Pro" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aciklama", "Fiyat", "Resim", "UrunAdi" },
                values: new object[] { "Apple iPhone X, OLED Super Retina ekran, Face ID ve çift arka kamera ile klasik bir model.", 29700.0, "iphone-x.webp", "iPhone X" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aciklama", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Samsung Galaxy S10, Dynamic AMOLED ekran, üçlü kamera ve kablosuz ters şarj özelliği.", 23100.0, 1, "samsung-galaxy-s10.webp", "Samsung Galaxy S10" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Aciklama", "Aktif", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Samsung Galaxy S8, kavisli Infinity Display ve IP68 su geçirmezlik standardı.", true, 16500.0, 1, "samsung-galaxy-s8.webp", "Samsung Galaxy S8" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Oppo F19 Pro Plus 5G, AMOLED ekran ve 65W SuperVOOC 2.0 hızlı şarj.", false, 13200.0, 1, "oppo-f19-pro-plus.webp", "Oppo F19 Pro Plus" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Aciklama", "Aktif", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Apple MacBook Pro 14 inç, M serisi çip, Liquid Retina XDR ekran ve uzun pil ömrü.", true, true, 66000.0, 2, "macbook-pro-14.webp", "MacBook Pro 14\" Space Grey" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Aciklama", "Aktif", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Asus Zenbook Pro çift ekranlı dizüstü, OLED ekran ve içerik üreticileri için tasarım.", true, true, 59400.0, 2, "asus-zenbook-pro.webp", "Asus Zenbook Pro Duo" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Huawei MateBook X Pro, 3K dokunmatik ekran, ince magnezyum kasa ve uzun pil ömrü.", false, 46200.0, 2, "huawei-matebook-x-pro.webp", "Huawei MateBook X Pro" });

            migrationBuilder.InsertData(
                table: "Urunler",
                columns: new[] { "Id", "Aciklama", "Aktif", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[,]
                {
                    { 9, "Dell XPS 13 9300, InfinityEdge 4K ekran ve Intel Core i7 işlemci.", true, false, 49500.0, 2, "dell-xps-13.webp", "Dell XPS 13 9300" },
                    { 10, "Lenovo Yoga 920 2-in-1 dönüştürülebilir dizüstü, dokunmatik ekran ve Active Pen desteği.", true, false, 36300.0, 2, "lenovo-yoga-920.webp", "Lenovo Yoga 920" },
                    { 11, "Apple iPad Mini 2021, A15 Bionic çip, 8.3 inç Liquid Retina ekran ve Apple Pencil 2 desteği.", true, true, 16500.0, 3, "ipad-mini-2021.webp", "iPad Mini 2021 Starlight" },
                    { 12, "Samsung Galaxy Tab S8+, 12.4 inç Super AMOLED ekran ve S Pen ile profesyonel kullanım.", true, true, 19800.0, 3, "galaxy-tab-s8-plus.webp", "Galaxy Tab S8+ Grey" },
                    { 13, "Samsung Galaxy Tab, geniş ekran ve uzun pil ömrü ile günlük kullanım için ideal tablet.", true, false, 11500.0, 3, "galaxy-tab-white.webp", "Galaxy Tab" },
                    { 14, "Pamuklu, regular fit mavi ve siyah kareli erkek gömlek — günlük şıklık.", true, false, 990.0, 4, "mavi-siyah-kareli-gomlek.webp", "Mavi & Siyah Kareli Gömlek" },
                    { 15, "Klasik ekose desenli uzun kollu erkek gömlek, slim fit kalıp.", true, false, 1155.0, 4, "ekoseli-gomlek.webp", "Ekoseli Erkek Gömlek" },
                    { 16, "Yaz aylarında konforlu kullanım için pamuklu kısa kollu erkek gömlek.", true, false, 660.0, 4, "kisa-kollu-gomlek.webp", "Kısa Kollu Erkek Gömlek" },
                    { 17, "Klasik kareli desen, regular fit erkek gömlek — ofis ve sosyal etkinlikler için.", true, false, 924.0, 4, "kareli-erkek-gomlek.webp", "Kareli Erkek Gömlek" },
                    { 18, "Gigabyte Aorus baskılı pamuklu gaming tişört, oyun severler için.", true, false, 825.0, 4, "aorus-tisort.webp", "Aorus Gaming Tişört" },
                    { 19, "Şık tasarımlı siyah uzun gece elbisesi — özel davetler ve gece etkinlikleri için.", true, true, 4290.0, 5, "siyah-gece-elbisesi.webp", "Siyah Uzun Gece Elbisesi" },
                    { 20, "Deri korse ve uyumlu etek takımı — modern ve iddialı bir görünüm.", true, true, 2970.0, 5, "deri-korse-etek.webp", "Deri Korse ve Etek Takım" },
                    { 21, "Marni tasarım kırmızı ve siyah iki parça takım, premium kumaş ve özel kesim.", true, false, 5940.0, 5, "marni-takim.webp", "Marni Kırmızı & Siyah Takım" },
                    { 22, "Hafif ve şık günlük elbise, sade tasarımıyla her ortama uygun.", true, false, 1650.0, 5, "pea-elbise.webp", "Pea Elbise" },
                    { 23, "Klasik siyah korse ve etek kombini — zarif ve feminen bir tarz.", true, false, 2640.0, 5, "siyah-korse-etek.webp", "Siyah Korse ve Etek" },
                    { 24, "Nike Air Jordan 1 ikonik kırmızı-siyah renk kombinasyonu, klasik basketbol siluet.", true, true, 4950.0, 6, "nike-air-jordan-1.webp", "Nike Air Jordan 1 Red & Black" },
                    { 25, "Puma Future Rider retro tasarım spor ayakkabı, hafif ve günlük konfor.", true, true, 2970.0, 6, "puma-future-rider.webp", "Puma Future Rider" },
                    { 26, "Off-white ve kırmızı renkli sneaker, modern siluet ve dayanıklı taban.", true, false, 3960.0, 6, "spor-ayakkabi-beyaz-kirmizi.webp", "Spor Ayakkabı Beyaz & Kırmızı" },
                    { 27, "Nike beyzbol ayakkabısı, sahada maksimum tutuş ve hareket kabiliyeti.", true, false, 2640.0, 6, "nike-baseball-cleats.webp", "Nike Baseball Cleats" },
                    { 28, "Off-white tonlarda spor ayakkabı, sokak modasına uygun şık tasarım.", true, false, 3630.0, 6, "spor-ayakkabi-off-white.webp", "Sports Sneakers Off White" },
                    { 29, "Longines Master Collection, İsviçre otomatik mekanizma ve klasik tasarım.", true, true, 49500.0, 7, "longines-master.webp", "Longines Master Collection" },
                    { 30, "Rolex Cellini Date, siyah kadran, otomatik mekanizma ve 18 ayar altın kasa.", true, true, 297000.0, 7, "rolex-cellini-date.webp", "Rolex Cellini Date Black" },
                    { 31, "Rolex Cellini Moonphase, ay safhası göstergesi ve premium İsviçre işçiliği.", true, false, 429000.0, 7, "rolex-cellini-moonphase.webp", "Rolex Cellini Moonphase" },
                    { 32, "Rolex Datejust, ikonik tasarım, otomatik mekanizma ve sürekli güncellenen tarih göstergesi.", true, false, 363000.0, 7, "rolex-datejust.webp", "Rolex Datejust" },
                    { 33, "Klasik tasarımlı kahverengi deri kayışlı kol saati, günlük şık kullanım.", true, false, 2970.0, 7, "kahverengi-deri-kayisli-saat.webp", "Kahverengi Deri Kayışlı Saat" },
                    { 34, "Chanel Coco Noir Eau de Parfum, gizemli ve oryantal kadın parfümü.", true, true, 4290.0, 8, "chanel-coco-noir.webp", "Chanel Coco Noir EDP" },
                    { 35, "Dior J'adore, çiçeksi ve feminen — klasik bir kadın parfümü.", true, true, 2970.0, 8, "dior-jadore.webp", "Dior J'adore EDP" },
                    { 36, "Gucci Bloom, beyaz çiçek kompozisyonu ile zarif kadın parfümü.", true, false, 2640.0, 8, "gucci-bloom.webp", "Gucci Bloom EDP" },
                    { 37, "Essence Lash Princess False Lash Effect maskara — yoğun ve uzun kirpik etkisi.", true, false, 330.0, 8, "essence-mascara.webp", "Essence Lash Princess Maskara" },
                    { 38, "Mat bitişli, uzun süre kalıcı klasik kırmızı ruj.", true, false, 429.0, 8, "kirmizi-ruj.webp", "Kırmızı Mat Ruj" },
                    { 39, "Prada deri kadın el çantası — lüks ve zarif tasarım.", true, true, 19800.0, 9, "prada-canta.webp", "Prada Kadın El Çantası" },
                    { 40, "Heshe markası gerçek deri kadın çanta, geniş hacim ve klasik tasarım.", true, false, 4290.0, 9, "heshe-deri-canta.webp", "Heshe Deri Kadın Çantası" },
                    { 41, "Mavi renk şık kadın el çantası — günlük ve özel etkinlikler için.", true, false, 1650.0, 9, "mavi-el-cantasi.webp", "Mavi Kadın El Çantası" },
                    { 42, "Klasik siyah çerçeveli güneş gözlüğü, UV400 koruma.", true, false, 990.0, 9, "siyah-gunes-gozlugu.webp", "Siyah Güneş Gözlüğü" },
                    { 43, "Yeşil ve siyah ton kombinli moda güneş gözlüğü — günlük şık aksesuar.", true, false, 1155.0, 9, "yesil-siyah-gozluk.webp", "Yeşil & Siyah Güneş Gözlüğü" },
                    { 44, "Annibale Colombo el yapımı çift kişilik yatak — İtalyan tasarımı ve premium malzeme.", true, true, 62700.0, 10, "annibale-colombo-yatak.webp", "Annibale Colombo Yatak" },
                    { 45, "Annibale Colombo lüks oturma grubu koltuk, hakiki deri kaplama.", true, true, 82500.0, 10, "annibale-colombo-koltuk.webp", "Annibale Colombo Koltuk" },
                    { 46, "Knoll Saarinen Executive konferans sandalyesi, ikonik orta yüzyıl modern tasarım.", true, false, 16500.0, 10, "knoll-saarinen-sandalye.webp", "Knoll Saarinen Sandalye" },
                    { 47, "African Cherry ahşap komodin, sade ve sıcak detaylarla yatak odasına şıklık.", true, false, 9900.0, 10, "komodin-african-cherry.webp", "Komodin African Cherry" },
                    { 48, "Modern tasarım masa lambası — çalışma masası ve okuma köşeleri için ideal aydınlatma.", true, false, 1650.0, 10, "masa-lambasi.webp", "Modern Masa Lambası" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Elektronik", "elektronik" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Beyaz Eşya", "beyaz-esya" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Giyim", "giyim" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kozmetik", "kozmetik" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kategori 1", "kategori-1" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kategori 2", "kategori-2" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kategori 3", "kategori-3" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kategori 4", "kategori-4" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "KategoriAdi", "Url" },
                values: new object[] { "Kategori 5", "kategori-5" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Aciklama", "Aktif", "Fiyat", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", false, 10000.0, "1.jpeg", "Apple Watch 7" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aciklama", "Fiyat", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", 20000.0, "2.jpeg", "Apple Watch 8" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aciklama", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", 30000.0, 2, "3.jpeg", "Apple Watch 9" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Aciklama", "Aktif", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", false, 40000.0, 2, "4.jpeg", "Apple Watch 10" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", true, 50000.0, 2, "5.jpeg", "Apple Watch 11" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Aciklama", "Aktif", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", false, false, 60000.0, 3, "6.jpeg", "Apple Watch 12" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Aciklama", "Aktif", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", false, false, 70000.0, 3, "7.jpeg", "Apple Watch 14" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "KategoriId", "Resim", "UrunAdi" },
                values: new object[] { "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Nobis quam accusamus neque tempore, consequatur dolor, nihil impedit recusandae ad adipisci eveniet libero ipsum quidem optio laboriosam, ea ipsa ducimus iusto?", true, 80000.0, 4, "8.jpeg", "Apple Watch 15" });
        }
    }
}
