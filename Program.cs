using dotnet_store.Models;
using dotnet_store.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailService, SmtpEmailService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ImageService>();
builder.Services.AddHttpContextAccessor();

// Uygulama oluşturucu (builder) ve servis kaydı
// MVC (Controller + Views) desteğini ekliyoruz
builder.Services.AddControllersWithViews();

// Veritabanı bağlamını (DbContext) yapılandırıyoruz
// - Bağlantı dizesi appsettings.json içinden okunuyor
// - Bu proje için Sqlite kullanılıyor
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

// ASP.NET Core Identity (kullanıcı ve rol yönetimi) ekleniyor
// Entity Framework ile Identity tabloları `DataContext` içinde saklanacak
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequiredLength = 7;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

    // Default User settings.
    // options.User.AllowedUserNameCharacters =
    //         "abcdefghijklmnopqrstuvwxyz0123456789";
    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 50;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;

});

var app = builder.Build();

// HTTP isteği boru hattını (middleware) yapılandırıyoruz
// Üretim ortamında hata sayfası ve HSTS kullan
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Varsayılan HSTS süresi 30 gündür. Üretimde ihtiyaçınıza göre ayarlayın.
    app.UseHsts();
}

// HTTPS yönlendirmesi: HTTP isteklerini HTTPS'ye yönlendirir
app.UseHttpsRedirection();

// Routing (yönlendirme) middleware'i etkinleştir
app.UseRouting();

app.UseAuthentication();

// Yetkilendirme middleware'i (Identity/Policy tabanlı erişim kontrolü)
app.UseAuthorization();

// Statik varlıkların eşlenmesi (özelleştirilmiş uzantı/metot)
app.MapStaticAssets();

// Örnek kategori URL'leri (örnekler, gerçek yönlendirme yukarıda tanımlı)
// urunler/telefon
// urunler/elektronik
// urunler/beyaz-esya

// Ürünler için kategori bazlı rota
app.MapControllerRoute(
    name: "urunler_by_kategori",
    pattern: "urunler/{url?}",
    defaults: new { controller = "Urun", action = "List" })
    .WithStaticAssets();

// Varsayılan rota: /Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

SeedDatabase.Initialize(app);

// Uygulamayı başlat
app.Run();
