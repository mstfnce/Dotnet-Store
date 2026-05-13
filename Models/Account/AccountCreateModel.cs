using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class AccountCreatModel
{
    [Required]
    [Display(Name = "Ad Soyad")]
    // [RegularExpression("^[a-zA-Z0-9]{3,20}$", ErrorMessage = "Kullanıcı adı 3-20 karakter arasında olmalı ve sadece harf, rakam veya alt çizgi içermelidir.")]
    public string AdSoyad { get; set; } = null!;
    [Required]
    [Display(Name = "Eposta")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Parola eşleşmiyor")]
    public string ConfirmPassword { get; set; } = null!;
}
