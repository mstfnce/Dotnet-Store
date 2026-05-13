using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class KategoriCreateModel
{

    [Required]
    [StringLength(20)]
    [Display(Name = "Kategori Adi")]
    public string KategoriAdi { get; set; } = null!;


    [Required]
    [StringLength(30)]
    [Display(Name = "URL")]
    public string Url { get; set; } = null!;
}