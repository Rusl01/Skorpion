using Microsoft.Build.Framework;

namespace Application.Models;

public class GamePlayer
{
    [Required] public int GameId { get; set; }

    public Game Game { get; set; }

    [Required] public int PlayerId { get; set; }

    public Player Player { get; set; }
}