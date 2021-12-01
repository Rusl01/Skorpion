using Microsoft.Build.Framework;

namespace Application.Models;

/// <summary>
/// Модель игры
/// </summary>
public class Game
{
    public int Id { get; set; }

    [Required] public string Title { get; set; }

    [Required] public DateTime ReleaseDate { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string Cover { get; set; }

    [Required] public double Price { get; set; }

    public ICollection<GameGenre> Genres { get; set; }
}