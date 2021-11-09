using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models;

[Table("games")]
public class Game
{
    [Column("id")] public int Id { get; set; }

    [Column("title")] public string Title { get; set; }

    [Column("release_date")] public DateTime ReleaseDate { get; set; }

    [Column("description")] public string Description { get; set; }

    [Column("cover")] public string Cover { get; set; }

    [Column("price")] public double Price { get; set; }

    public ICollection<GameGenre> Genres { get; set; }
}