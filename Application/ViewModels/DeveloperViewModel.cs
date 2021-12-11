using Application.Models;

namespace Application.ViewModels;

/// <summary>
/// Модель для страницы "Студия разработчика"
/// </summary>
public class DeveloperViewModel
{
    public List<Game> Games { get; set; }
    public PageViewModel PageViewModel { get; set; }
}