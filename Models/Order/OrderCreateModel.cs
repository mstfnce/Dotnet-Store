using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class OrderCreateModel
{

    [Required(ErrorMessage = "{0} alanı zorunludur.")]
    [StringLength(100, ErrorMessage = "{0} en fazla {1} karakter olabilir.", MinimumLength = 3)]
    [Display(Name = "Ad Soyad")]
    public string AdSoyad { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı zorunludur.")]
    [StringLength(50, ErrorMessage = "{0} en fazla {1} karakter olabilir.", MinimumLength = 2)]
    [Display(Name = "Şehir")]
    public string Sehir { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı zorunludur.")]
    [StringLength(250, ErrorMessage = "{0} en fazla {1} karakter olabilir.", MinimumLength = 10)]
    [Display(Name = "Adres Satırı")]
    public string AdresSatiri { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı zorunludur.")]
    [RegularExpression(@"^\d{5}$", ErrorMessage = "{0} 5 haneli olmalıdır.")]
    [Display(Name = "Posta Kodu")]
    public string PostaKodu { get; set; } = null!;

    [Required(ErrorMessage = "{0} alanı zorunludur.")]
    [Phone(ErrorMessage = "Geçerli bir {0} giriniz.")]
    [StringLength(20, ErrorMessage = "{0} en fazla {1} karakter olabilir.", MinimumLength = 10)]
    [Display(Name = "Telefon")]
    public string Telefon { get; set; } = null!;

    [StringLength(500, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
    [Display(Name = "Sipariş Notu")]
    public string? SiparisNotu { get; set; }
}
