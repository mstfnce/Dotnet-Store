using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_store.Models;
using dotnet_store.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class AccountController : Controller
{
    // Kullanıcıları yöneten servis (Identity UserManager)
    // Bu servis Dependency Injection (DI) ile sağlanır
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private IEmailService _emailService;
    private readonly DataContext _context;
    private readonly ICartService _cartService;

    // Constructor: DI container buraya UserManager örneğini verir
    // Diğer action'larda kullanmak için özel alana atıyoruz
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, DataContext context, ICartService cartService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _context = context;
        _cartService = cartService;
    }

    // GET: /Account/Create
    // Kayıt formunu gösterir
    public ActionResult Create()
    {
        return View();
    }

    // POST: /Account/Create
    // Kullanıcı oluşturma formunu işler
    [HttpPost]
    public async Task<ActionResult> Create(AccountCreatModel model)
    {
        // Model doğrulamasını kontrol et (veri anotasyonları)
        if (ModelState.IsValid)
        {
            // Form verilerinden yeni bir IdentityUser oluştur
            var user = new AppUser { UserName = model.Email, Email = model.Email, AdSoyad = model.AdSoyad };

            // Kullanıcıyı veritabanına parola ile birlikte kaydet
            var result = await _userManager.CreateAsync(user, model.Password);

            // Başarılıysa anasayfaya yönlendir
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            // Hataları ModelState'a ekle, böylece formda gösterilir
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

        }

        // Hata varsa formu tekrar göster (doğrulama mesajlarıyla)
        return View(model);
    }


    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AccountLoginModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.BeniHatirla, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    await _cartService.TransferCartToUser(user.UserName!);



                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                    ModelState.AddModelError("", $"Hesabiniz kitlendi. Lutfen {timeLeft.Minutes + 1} dk sonra tekrar deneyiniz");

                }
                else
                {
                    ModelState.AddModelError("", "Hatalı parola");
                }
            }
            else
            {
                ModelState.AddModelError("", "Hatalı email");
            }
        }

        return View(model);
    }


    [Authorize]
    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    [Authorize]
    public ActionResult Settings()
    {
        return View();
    }


    [Authorize]
    public async Task<ActionResult> EditUser()
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new AccountEditUserModel
        {
            AdSoyad = user.AdSoyad,
            Email = user.Email!
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> EditUser(AccountEditUserModel model)
    {

        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user != null)
            {
                user.AdSoyad = model.AdSoyad;
                user.Email = model.Email;
                user.UserName = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["Mesaj"] = "Kullanici bilgileri guncellendi.";
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

        }
        return View(model);
    }


    [Authorize]
    public ActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> ChangePasswordAsync(AccountChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user != null)
            {


                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if (result.Succeeded)
                {
                    TempData["Mesaj"] = "Parolaniz guncellendi.";
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }

    public ActionResult AccessDenied()
    {
        return View();
    }


    public ActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["Mesaj"] = "E posta adresi girmelisiniz.";
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email.Trim());

        if (user == null)
        {
            TempData["Mesaj"] = "Bu e posta adresi ile kayitli kullanici bulunamadi.";
            return View();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //send mail
        var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token });
        var link = $"<a href='http://localhost:5162{url}'>Parola sifirlama baglantisi</a>";

        await _emailService.SendEmailAsync(user.Email!, "Parola sifirlama baglantisi", link);


        TempData["Mesaj"] = "E-posta adresinize gonderilen link ile sifrenizi sifirlayabilirsiniz.";

        return View();
    }



    public async Task<ActionResult> ResetPassword(string userId, string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
        {
            TempData["Mesaj"] = "Parola sifirlama baglantisi gecersiz.";
            return RedirectToAction("Login");
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            TempData["Mesaj"] = "Kullanici bulunamadi.";
            return RedirectToAction("Login");
        }


        var model = new AccountResetPasswordModel
        {
            Token = token,
            Email = user.Email!
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> ResetPassword(AccountResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Token))
            {
                TempData["Mesaj"] = "Parola sifirlama baglantisi gecersiz.";
                return RedirectToAction("ForgotPassword");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                TempData["Mesaj"] = "Kullanici bulunamadi.";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData["Mesaj"] = "Parolaniz guncellendi. Yeni parolaniz ile giris yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        return View(model);
    }
}
