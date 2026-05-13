using System.ComponentModel.DataAnnotations;

namespace dotnet_store.Models;

public class RoleCreateModel
{

    [Required]
    [StringLength(20)]
    [Display(Name = "Role Adi")]
    public string RoleAdi { get; set; } = null!;


}