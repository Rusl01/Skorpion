using System.Diagnostics;
using Application.Data;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers;

/// <summary>
/// Контроллер для управления главной страницей
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;
    
    public HomeController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _logger = logger;
        _db = context;
    }

    /// <summary>
    /// Главная страница
    /// </summary>
    public IActionResult Index()
    {
        var games = _db.Games.ToList();
        return View(games);
    }

    /// <summary>
    /// Страница с ошибкой
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}