namespace dotnet_store.Models;

// Tamamlanmış sipariş. Cart'a çok benzer ama kalıcıdır: checkout sırasında
// sepetteki ürünler ve o anki fiyatlarıyla birlikte buraya "donuk" olarak kopyalanır
// (Urun.Fiyat sonradan değişse bile OrderItem.Fiyat sipariş anındaki fiyatı korur).
public class Order
{
    public int Id { get; set; }
    public DateTime SiparisTarihi { get; set; }
    public string AdSoyad { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Sehir { get; set; } = null!;
    public string AdresSatiri { get; set; } = null!;
    public string PostaKodu { get; set; } = null!;
    public string Telefon { get; set; } = null!;
    public double ToplamFiyat { get; set; }
    public string SiparisNotu { get; set; } = null!;

    public List<OrderItem> OrderItems { get; set; } = new();

    public double AraToplam()
    {
        return OrderItems.Sum(i => i.Fiyat * i.Miktar);
    }

    public double Toplam()
    {
        return OrderItems.Sum(i => i.Fiyat * i.Miktar) * 1.2;
    }
}

// Siparişteki tek bir satır: hangi ürün, kaç adet, hangi fiyattan alındı.
// Order (1) - OrderItem (N) ilişkisinin "çok" tarafı.
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int UrunId { get; set; }
    public Urun Urun { get; set; } = null!;
    public double Fiyat { get; set; }
    public int Miktar { get; set; }
}