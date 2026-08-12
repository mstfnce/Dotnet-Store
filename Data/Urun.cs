namespace dotnet_store.Models;

// Mağazadaki ürünler. KategoriId foreign key ile bir Kategori'ye bağlıdır (N:1).
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; } = null!;
    public double Fiyat { get; set; }
    public string? Resim { get; set; }
    public string? Aciklama { get; set; }
    public bool Aktif { get; set; }
    public bool Anasayfa { get; set; }
    public int KategoriId { get; set; }
    public Kategori Kategori { get; set; } = null!;
}
