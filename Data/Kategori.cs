namespace dotnet_store.Models;


//entity: veritabani tablosu
public class Kategori
{
    public int Id { get; set; }
    public string KategoriAdi { get; set; } = null!;
    public string Url { get; set; } = null!;
    public List<Urun> Uruns { get; set; } = new();
}