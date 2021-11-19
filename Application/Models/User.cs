using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Application.Models;

public class User : IdentityUser
{
    [Required]
    [Column("nickname")]
    [Display(Name = "Nickname")]
    public string Nickname { get; set; }
    [Display(Name = "UserPhotoUrl")] 
    public string UserPhotoUrl { get; set; }
}