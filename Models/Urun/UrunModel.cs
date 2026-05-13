using System.ComponentModel.DataAnnotations;
namespace dotnet_store.Models;

public class UrunModel
{
    [Display(Name = "Urun adi")]
    [Required(ErrorMessage = "{0} girmelisiniz.")]
    [StringLength(50, ErrorMessage = "{0} icin {2}-{1} karakter araliginda deger girmelisiniz.", MinimumLength = 10)]
    public string UrunAdi { get; set; } = null!;

    [Display(Name = "Fiyat")]
    [Required(ErrorMessage = "{0} girmelisiniz.")]
    [Range(0, 1000000, ErrorMessage = "{0} icin {1}-{2} arasinda deger girmelisiniz.")]
    public double? Fiyat { get; set; }

    [Display(Name = "Urun Resim")]
    public IFormFile? Resim { get; set; }

    [Display(Name = "Urun Aciklama")]
    public string? Aciklama { get; set; }

    [Display(Name = "Aktif")]
    public bool Aktif { get; set; }

    [Display(Name = "Anasayfa")]
    public bool Anasayfa { get; set; }

    [Display(Name = "Kategori ")]
    [Required(ErrorMessage = "{0} girmelisiniz.")]
    public int? KategoriId { get; set; }
}