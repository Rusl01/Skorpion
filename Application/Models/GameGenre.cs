using Microsoft.Build.Framework;

namespace Application.Models;

/// <summary>
/// Модель связи "многие-ко-многим" жанры игр
/// </summary>
public class GameGenre
{
    [Required] public int GameId { get; set; }

    public Game Game { get; set; }

    [Required] public int GenreId { get; set; }

    public Genre Genre { get; set; }
}