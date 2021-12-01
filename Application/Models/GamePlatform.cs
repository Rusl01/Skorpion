using Microsoft.Build.Framework;

namespace Application.Models;

/// <summary>
/// Модель связи "многие-ко-многим" платформ игр
/// </summary>
public class GamePlatform
{
    [Required] public int GameId { get; set; }

    public Game Game { get; set; }

    [Required] public int PlatformId { get; set; }

    public Platform Platform { get; set; }
}