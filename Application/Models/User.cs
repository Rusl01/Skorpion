using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Application.Models;

public class User : IdentityUser
{
    [Required] [Column("Nickname")] public string Nickname { get; set; }
    public string UserPhotoUrl { get; set; }
}