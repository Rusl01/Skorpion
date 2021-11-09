using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models;

[Table("games_players")]
public class GamePlayer
{
    [Column("game_id")] public int GameId { get; set; }

    public Game Game { get; set; }

    [Column("player_id")] public int PlayerId { get; set; }

    public Player Player { get; set; }
}