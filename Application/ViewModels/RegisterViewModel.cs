using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

public class RegisterViewModel
{
    public string UserPhotoUrl { get; set; }
    [Required] public string Nickname { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
    public string ConfirmPassword { get; set; }
}