using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Application.Models;

public class Player
{
    public int Id { get; set; }
    [Required] public string Name { get; set; }

    [NotMapped] public bool Selected { get; set; }

    public ICollection<GamePlayer> Games { get; set; }
}