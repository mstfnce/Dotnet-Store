namespace dotnet_store.Models;

// Ürün kategorileri (Telefon, Bilgisayar, Ayakkabı vb.).
// Kategori (1) - Urun (N) ilişkisinin "bir" tarafı: bir kategoriye birden çok ürün bağlanabilir.
// Url alanı /urunler/{url} rotasında kategoriye göre filtreleme için kullanılıyor.
public class Kategori
{
    public int Id { get; set; }
    public string KategoriAdi { get; set; } = null!;
    public string Url { get; set; } = null!;
    public List<Urun> Uruns { get; set; } = new();
}