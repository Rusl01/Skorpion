using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы оформления заказа
/// </summary>
public class CheckoutViewModel
{
    [Required] public List<CartItem> CartItems { get; set; }
    [Required] public int Count { get; set; }
    [Required] public double Total { get; set; }
}