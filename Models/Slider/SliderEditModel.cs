using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class SliderEditModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Başlık")]
    public string Baslik { get; set; } = null!;

    [StringLength(500)]
    [Display(Name = "Açıklama")]
    public string? Aciklama { get; set; }

    [Display(Name = "Resim")]
    public IFormFile? Resim { get; set; }

    public string? ResimAdi { get; set; }

    [Required]
    [Display(Name = "Sıra")]
    public int? Index { get; set; }

    [Display(Name = "Aktif")]
    public bool Aktif { get; set; }
}
