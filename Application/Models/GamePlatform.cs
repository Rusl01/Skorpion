using Microsoft.Build.Framework;

namespace Application.Models;

public class GamePlatform
{
    [Required] public int GameId { get; set; }

    public Game Game { get; set; }

    [Required] public int PlatformId { get; set; }

    public Platform Platform { get; set; }
}