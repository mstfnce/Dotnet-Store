using System.Security.Claims;
using dotnet_store.Models;
using dotnet_store.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Tests;

public class CartServiceTests
{
    // Her testin kendi bagimsiz "sahte veritabani"na sahip olmasi icin
    // her seferinde farkli bir isimle (Guid) yeni bir in-memory DataContext olusturuyoruz.
    // Boylece testler birbirini etkilemez.
    private static DataContext NewInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new DataContext(options);
    }

    // Gercek bir tarayici/HTTP istegi olmadan, "sanki su kullanici giris yapmis gibi"
    // davranan sahte bir IHttpContextAccessor olusturur.
    private static IHttpContextAccessor FakeAccessor(string? girisYapmisKullaniciAdi = null)
    {
        var httpContext = new DefaultHttpContext();

        if (girisYapmisKullaniciAdi != null)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, girisYapmisKullaniciAdi) }, "TestAuth");
            httpContext.User = new ClaimsPrincipal(identity);
        }

        return new HttpContextAccessor { HttpContext = httpContext };
    }

    [Fact]
    public async Task GetCart_YeniMusteri_YeniSepetOlusturur()
    {
        // Arrange: bomboş bir sahte veritabani, "testuser" olarak giris yapmis kullanici
        var dbName = Guid.NewGuid().ToString();
        var context = NewInMemoryContext(dbName);
        var service = new CartService(context, FakeAccessor("testuser"));

        // Act
        var cart = await service.GetCart("testuser");

        // Assert
        Assert.Equal("testuser", cart.CustomerId); // yeni sepet, dogru musteriye ait olmali
        Assert.Empty(cart.CartItems); // yeni sepet bos baslamali
    }

    [Fact]
    public async Task GetCart_VarOlanMusteri_MevcutSepetiGetirir()
    {
        // Arrange: veritabaninda "existing-user" icin onceden kaydedilmis bir sepet olsun
        var dbName = Guid.NewGuid().ToString();
        var setupContext = NewInMemoryContext(dbName);
        setupContext.Carts.Add(new Cart { CustomerId = "existing-user" });
        await setupContext.SaveChangesAsync();

        // Ayni veritabanina (ayni dbName) baglanan YENI bir context ve servis olusturuyoruz,
        // boylece gercekten veritabanindan okundugunu (hafizadaki eski nesneden degil) kanitlamis oluruz.
        var context = NewInMemoryContext(dbName);
        var service = new CartService(context, FakeAccessor());

        // Act
        var cart = await service.GetCart("existing-user");

        // Assert
        Assert.Equal("existing-user", cart.CustomerId); // dogru musterinin sepeti getirilmeli
        var toplamSepetSayisi = await context.Carts.CountAsync();
        Assert.Equal(1, toplamSepetSayisi); // yeni bir sepet ACILMAMALI, olan sepet kullanilmali
    }

    [Fact]
    public async Task AddToCart_UrunuSepeteEklerVeVeritabaninaKaydeder()
    {
        // Arrange: veritabaninda bir Urun olsun, "testuser" giris yapmis olsun
        var dbName = Guid.NewGuid().ToString();
        var setupContext = NewInMemoryContext(dbName);
        setupContext.Urunler.Add(new Urun { Id = 1, UrunAdi = "Test Urun", Fiyat = 100 });
        await setupContext.SaveChangesAsync();

        var context = NewInMemoryContext(dbName);
        var service = new CartService(context, FakeAccessor("testuser"));

        // Act
        await service.AddToCart(urunId: 1, miktar: 2);

        // Assert: BASKA bir context ile ayni veritabanina bakiyoruz,
        // boylece SaveChangesAsync'in gercekten calisip kaydettigini kanitlamis oluyoruz.
        var kontrolContext = NewInMemoryContext(dbName);
        var kaydedilenSepet = await kontrolContext.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CustomerId == "testuser");

        Assert.NotNull(kaydedilenSepet);
        Assert.Single(kaydedilenSepet!.CartItems);
        Assert.Equal(2, kaydedilenSepet.CartItems[0].Miktar);
    }

    [Fact]
    public async Task RemoveItem_UrunMiktariniAzaltirVeKaydeder()
    {
        // Arrange: "testuser" adinda kullanicinin sepetinde onceden 5 adet urun olsun
        var dbName = Guid.NewGuid().ToString();
        var setupContext = NewInMemoryContext(dbName);
        setupContext.Urunler.Add(new Urun { Id = 1, UrunAdi = "Test Urun", Fiyat = 100 });
        setupContext.Carts.Add(new Cart
        {
            CustomerId = "testuser",
            CartItems = new List<CartItem> { new CartItem { UrunId = 1, Miktar = 5 } }
        });
        await setupContext.SaveChangesAsync();

        var context = NewInMemoryContext(dbName);
        var service = new CartService(context, FakeAccessor("testuser"));

        // Act: 2 adet cikar
        await service.RemoveItem(urunId: 1, miktar: 2);

        // Assert: baska bir context ile gercekten kaydedildigini dogruluyoruz
        var kontrolContext = NewInMemoryContext(dbName);
        var kaydedilenSepet = await kontrolContext.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CustomerId == "testuser");

        Assert.NotNull(kaydedilenSepet);
        Assert.Single(kaydedilenSepet!.CartItems); // 3 kaldigi icin satir hala duruyor olmali
        Assert.Equal(3, kaydedilenSepet.CartItems[0].Miktar); // 5 - 2 = 3
    }

    [Fact]
    public async Task TransferCartToUser_MisafirSepetiniKullaniciSepetineTasir()
    {
        // Arrange:
        // - "realuser" sepetinde zaten 1 tane Urun#1 var
        // - misafir (cookie) sepetinde 2 tane Urun#1 (birlesecek) ve 5 tane Urun#2 (yeni eklenecek) var
        var dbName = Guid.NewGuid().ToString();
        var setupContext = NewInMemoryContext(dbName);
        setupContext.Urunler.AddRange(
            new Urun { Id = 1, UrunAdi = "Urun A", Fiyat = 100 },
            new Urun { Id = 2, UrunAdi = "Urun B", Fiyat = 50 }
        );
        setupContext.Carts.Add(new Cart
        {
            CustomerId = "realuser",
            CartItems = new List<CartItem> { new CartItem { UrunId = 1, Miktar = 1 } }
        });
        setupContext.Carts.Add(new Cart
        {
            CustomerId = "guest-cookie-id",
            CartItems = new List<CartItem>
            {
                new CartItem { UrunId = 1, Miktar = 2 },
                new CartItem { UrunId = 2, Miktar = 5 }
            }
        });
        await setupContext.SaveChangesAsync();

        // Misafirin tarayicisinda "customerId=guest-cookie-id" cookie'si var gibi davraniyoruz
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Cookie"] = "customerId=guest-cookie-id";

        var context = NewInMemoryContext(dbName);
        var service = new CartService(context, new HttpContextAccessor { HttpContext = httpContext });

        // Act: kullanici giris yapinca cagirilan aktarma islemi
        await service.TransferCartToUser("realuser");

        // Assert
        var kontrolContext = NewInMemoryContext(dbName);
        var userCart = await kontrolContext.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.CustomerId == "realuser");

        Assert.NotNull(userCart);
        Assert.Equal(2, userCart!.CartItems.Count); // Urun1 (birlesmis) + Urun2 (yeni) = 2 satir

        var urun1 = userCart.CartItems.First(i => i.UrunId == 1);
        Assert.Equal(3, urun1.Miktar); // 1 (eskiden vardi) + 2 (misafirden geldi) = 3

        var urun2 = userCart.CartItems.First(i => i.UrunId == 2);
        Assert.Equal(5, urun2.Miktar); // yeni urun oldugu gibi eklenmis olmali

        var misafirSepetiHalaVarMi = await kontrolContext.Carts.AnyAsync(c => c.CustomerId == "guest-cookie-id");
        Assert.False(misafirSepetiHalaVarMi); // misafir sepeti aktarildiktan sonra silinmis olmali
    }
}
