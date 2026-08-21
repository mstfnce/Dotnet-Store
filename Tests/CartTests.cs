using dotnet_store.Models;

namespace dotnet_store.Tests;

public class CartTests
{
    [Fact]
    public void AddItem_YeniUrun_SepeteEkler()
    {
        // Arrange
        var cart = new Cart();
        var urun = new Urun { Id = 1, UrunAdi = "Test Urun", Fiyat = 100 };

        // Act
        cart.AddItem(urun, 2);

        // Assert
        Assert.Single(cart.CartItems); // listede tam olarak 1 satir olmali (yeni urun icin acilan tek satir)
        Assert.Equal(2, cart.CartItems[0].Miktar); // o satirdaki miktar 2 olmali
    }

    [Fact]
    public void AddItem_VarOlanUrun_MiktariArtirir()
    {
        // Arrange
        var cart = new Cart();
        var urun = new Urun { Id = 1, UrunAdi = "Test Urun", Fiyat = 100 };
        cart.AddItem(urun, 1);

        // Act
        cart.AddItem(urun, 3);

        // Assert
        Assert.Single(cart.CartItems); // ayni urun tekrar eklendi, yeni satir acilmamali, hala 1 satir olmali
        Assert.Equal(4, cart.CartItems[0].Miktar); // miktarlar birikmeli: 1 + 3 = 4
    }

    [Fact]
    public void DeleteItem_MiktarSifirinUstundeKalirsa_SatiriKorur()
    {
        // Arrange
        var cart = new Cart();
        var urun = new Urun { Id = 1, UrunAdi = "Test Urun", Fiyat = 100 };
        cart.AddItem(urun, 5);

        // Act
        cart.DeleteItem(urun.Id, 2);

        // Assert
        Assert.Single(cart.CartItems); // miktar hala sifirin ustunde, satir silinmemis olmali
        Assert.Equal(3, cart.CartItems[0].Miktar); // 5 - 2 = 3 kalmali
    }

    [Fact]
    public void DeleteItem_MiktarSifiraDusunce_SatiriSepettenSiler()
    {
        // Arrange
        var cart = new Cart();
        var urun = new Urun { Id = 1, UrunAdi = "Test Urun", Fiyat = 100 };
        cart.AddItem(urun, 2);

        // Act
        cart.DeleteItem(urun.Id, 2);

        // Assert
        Assert.Empty(cart.CartItems); // liste bombos olmali: miktar 0'a dusunce satir tamamen silinir
    }

    [Fact]
    public void AraToplam_UrunlerinFiyatXMiktarToplaminiVerir()
    {
        // Arrange
        var cart = new Cart();
        cart.AddItem(new Urun { Id = 1, UrunAdi = "Urun A", Fiyat = 100 }, 2); // 200
        cart.AddItem(new Urun { Id = 2, UrunAdi = "Urun B", Fiyat = 50 }, 3);  // 150

        // Act
        var araToplam = cart.AraToplam();

        // Assert
        Assert.Equal(350, araToplam); // (100*2) + (50*3) = 350 olmali, KDV yok
    }

    [Fact]
    public void Toplam_AraToplamaYuzde20KdvEkler()
    {
        // Arrange
        var cart = new Cart();
        cart.AddItem(new Urun { Id = 1, UrunAdi = "Urun A", Fiyat = 100 }, 1); // 100

        // Act
        var toplam = cart.Toplam();

        // Assert
        Assert.Equal(120, toplam); // 100 * 1.2 = 120, yani ustune %20 kdv binmis olmali
    }
}
