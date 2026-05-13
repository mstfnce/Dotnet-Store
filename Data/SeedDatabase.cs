using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_store.Models;

public static class SeedDatabase
{
    public static async void Initialize(IApplicationBuilder app)
    {
        var userManager = app.ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<AppUser>>();

        var roleManager = app.ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<AppRole>>();

        if (!roleManager.Roles.Any())
        {
            var admin = new AppRole { Name = "Admin" };
            await roleManager.CreateAsync(admin);
        }

        if (!userManager.Users.Any())
        {
            var admin = new AppUser
            {
                AdSoyad = "Mustafa Ince",
                UserName = "mustafaince",
                Email = "mustafance52@gmail.com"
            };

            await userManager.CreateAsync(admin, "12345678");
            await userManager.AddToRoleAsync(admin, "Admin");

            var customer = new AppUser
            {
                AdSoyad = "John Wick",
                UserName = "johnwick",
                Email = "johnwick52m@gmail.com"
            };

            await userManager.CreateAsync(customer, "12345678");
        }
    }
}
