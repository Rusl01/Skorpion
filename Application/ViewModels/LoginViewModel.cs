using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы авторизации
/// </summary>
public class LoginViewModel
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember Me")] public bool RememberMe { get; set; }
}