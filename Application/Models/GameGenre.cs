using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    [Table("games_genres")]
    public class GameGenre
    {
        [Column("game_id")]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Column("genre_id")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}