using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.ViewModels;

public class ProfileViewModel
{
    [Required] public User User { get; set; }
    [Required] public Dictionary<Guid, Game> KeyGames { get; set; }
    [Required] public bool AddFriend { get; set; }
    [Required] public List<User> Friends { get; set; }
}