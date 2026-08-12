using Microsoft.AspNetCore.Identity;

namespace dotnet_store.Models;

// ASP.NET Identity'nin hazır IdentityUser<int> sınıfından türetilmiş kullanıcı modeli.
// UserName, Email, PasswordHash gibi alanlar IdentityUser'dan gelir; AdSoyad bizim eklediğimiz alan.
// Bu sayede login/register/şifre sıfırlama gibi işler Identity kütüphanesi tarafından hazır sağlanır.
public class AppUser : IdentityUser<int>
{
    public string AdSoyad { get; set; } = null!;
}