namespace dotnet_store.Models;


// model: veri tasimak icin
public class KategoriGetModel
{
    public int Id { get; set; }
    public string KategoriAdi { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int UrunSayisi { get; set; }
}