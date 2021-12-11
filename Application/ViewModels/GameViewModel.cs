using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Models;


namespace Application.ViewModels;

/// <summary>
/// Модель для страниц создания и обновления игры
/// </summary>
public class GameViewModel
{
    [Required] public int Id { get; set; }
    [Required] public string Title { get; set; }

    [Required] public DateTime ReleaseDate { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string Cover { get; set; }

    [Required] public double Price { get; set; }
    
    [Required] public List<Genre> Genres { get; set; }
    [Required] public List<Platform> Platforms { get; set; }
    [Required] public List<Player> Players { get; set; }
    [Required] public string DeveloperSite { get; set; }
}