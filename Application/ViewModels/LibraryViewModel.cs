using System.Collections;
using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.ViewModels;

public class LibraryViewModel
{
    [Required] public Dictionary<Guid, Game> KeyGames { get; set; }
}