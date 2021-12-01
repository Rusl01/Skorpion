using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Application.Models;

/// <summary>
/// Модель жанра игры
/// </summary>
public class Genre
{
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    [NotMapped] public bool Selected { get; set; }

    public ICollection<GameGenre> Games { get; set; }
}