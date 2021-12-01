using System.ComponentModel.DataAnnotations;

namespace Application.Models;

/// <summary>
/// Модель предмета в корзине для авторизованного пользователя
/// </summary>
public class CartItem
{
    [Key] public int ItemId { get; set; }
    public string UserId { get; set; }
    public string GameId { get; set; }
}