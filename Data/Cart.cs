using System.Xml.Serialization;

namespace dotnet_store.Models;

// Alışveriş sepeti. Kullanıcı giriş yapmışsa CustomerId = kullanıcı adı,
// yapmamışsa tarayıcıya yazılan "customerId" cookie değeridir (bkz. CartService).
// Sepetle ilgili ekleme/silme/toplam hesaplama mantığı burada, controller'da değil.
public class Cart
{
    public int CartId { get; set; }
    public string CustomerId { get; set; } = null!;

    public List<CartItem> CartItems { get; set; } = new();

    public void AddItem(Urun urun, int miktar)
    {
        var item = CartItems.FirstOrDefault(i => i.UrunId == urun.Id);

        if (item == null)
        {
            CartItems.Add(new CartItem { UrunId = urun.Id, Urun = urun, Miktar = miktar });
        }
        else
        {
            item.Miktar += miktar;
        }
    }


    public void DeleteItem(int urunId, int miktar)
    {
        var item = CartItems.FirstOrDefault(i => i.UrunId == urunId);

        if (item != null)
        {
            item.Miktar -= miktar;

            if (item.Miktar <= 0)
            {
                CartItems.Remove(item);
            }
        }
    }





    public double AraToplam()
    {
        return CartItems.Sum(i => i.Urun.Fiyat * i.Miktar);
    }

    public double Toplam()
    {
        return CartItems.Sum(i => i.Urun.Fiyat * i.Miktar) * 1.2;
    }
}

// Sepetteki tek bir satır: hangi üründen kaç adet.
// Cart (1) - CartItem (N) ilişkisinin "çok" tarafı.
public class CartItem
{
    public int CartItemId { get; set; }


    public int UrunId { get; set; }
    public Urun Urun { get; set; } = null!;


    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;


    public int Miktar { get; set; }
}

