using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.ViewModels;

public class ProfileViewModel
{
    [Required]
    public User User { get; set; }
    [Required]
    public Dictionary<Guid, Game> KeyGames { get; set; }
}