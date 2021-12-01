using Application.Models;
using Microsoft.Build.Framework;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы поиска пользователя
/// </summary>
public class FindUserViewModel
{
    [Required] public List<User> Users { get; set; }
    [Required] public string Nickname { get; set; }
}