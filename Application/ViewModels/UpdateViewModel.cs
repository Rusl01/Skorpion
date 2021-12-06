using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы редактирования пользователя
/// </summary>
public class UpdateViewModel
{
    public string UserPhotoUrl { get; set; }
    [Required] public string Nickname { get; set; }
    [Required] [EmailAddress] public string Email { get; set; }
}