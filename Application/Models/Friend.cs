using Microsoft.Build.Framework;

namespace Application.Models;

/// <summary>
/// Модель связи двух пользователей как друзей
/// </summary>
public class Friend
{
    [Required] public string FirstUserId { get; set; }
    public User FirstUser { get; set; }
    [Required] public string SecondUserId { get; set; }
    public User SecondUser { get; set; }
}