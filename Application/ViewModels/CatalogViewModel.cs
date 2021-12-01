using Application.Models;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы каталога
/// </summary>
public class CatalogViewModel
{
    public List<Game> Games { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Platform> Platforms { get; set; }
    public List<Player> Players { get; set; }
}