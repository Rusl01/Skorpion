using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models;

[Table("genres")]
public class Genre
{
    [Column("id")] public int Id { get; set; }

    [Column("name")] public string Name { get; set; }

    [NotMapped] public bool Selected { get; set; }

    public ICollection<GameGenre> Games { get; set; }
}