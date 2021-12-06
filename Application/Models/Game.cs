using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models;

/// <summary>
/// Модель игры
/// </summary>
public class Game
{
    [ScaffoldColumn(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }

    [Microsoft.Build.Framework.Required] public string Title { get; set; }

    [Microsoft.Build.Framework.Required] public DateTime ReleaseDate { get; set; }

    [Microsoft.Build.Framework.Required] public string Description { get; set; }

    [Microsoft.Build.Framework.Required] public string Cover { get; set; }

    [Microsoft.Build.Framework.Required] public double Price { get; set; }
    
    [Microsoft.Build.Framework.Required] public User Developer { get; set; }

    public ICollection<GameGenre> Genres { get; set; }
    public ICollection<GamePlatform> Platforms { get; set; }
    public ICollection<GamePlayer> Players { get; set; }
}