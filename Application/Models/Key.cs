using Microsoft.Build.Framework;

namespace Application.Models;

public class Key
{
    [Required] public Guid Id { get; set; }
    [Required] public string UserId { get; set; }
    [Required] public int GameId { get; set; }
}