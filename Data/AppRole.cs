using Microsoft.AspNetCore.Identity;

namespace dotnet_store.Models;

// ASP.NET Identity'nin hazır IdentityRole<int> sınıfından türetilmiş rol modeli (ör. "Admin").
// AppUser <-> AppRole ilişkisi Identity'nin kendi ara tablosu (UserRoles) üzerinden N:N kurulur.
public class AppRole : IdentityRole<int>
{

}