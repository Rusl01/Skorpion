using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    [Table("games_platforms")]
    public class GamePlatform
    {
        [Column("game_id")]
        public int GameId { get; set; }
        public Game Game { get; set; }
        
        [Column("platform_id")]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}