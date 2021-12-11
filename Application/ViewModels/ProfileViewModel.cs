using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы профиля
/// </summary>
public class ProfileViewModel
{
    [Required] public User User { get; set; }
    [Required] public ICollection<Game> Games { get; set; }
    [Required] public bool AddFriend { get; set; }
    [Required] public ICollection<User> Friends { get; set; }
}