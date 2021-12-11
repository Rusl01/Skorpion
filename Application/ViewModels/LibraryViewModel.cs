using System.Collections;
using System.ComponentModel.DataAnnotations;
using Application.Models;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы "Библиотека игр" пользователя
/// </summary>
public class LibraryViewModel
{
    [Required] public Dictionary<Guid, Game> KeyGames { get; set; }
    public PageViewModel PageViewModel { get; set; }
}