using Application.Data;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Контроллер для управления каталогом
/// </summary>
public class CatalogController : Controller
{
    private readonly ApplicationContext _db;

    public CatalogController(ApplicationContext context)
    {
        _db = context;
    }

    /// <summary>
    /// Страница каталога
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index(string sortOrder, string searchString)
    {
        var games = SortSearchGames(sortOrder, searchString);
        var genres = _db.Genres.ToList();
        var platforms = _db.Platforms.ToList();
        var players = _db.Players.ToList();
        
        var catalogViewModel = new CatalogViewModel
        {
            Games = games.ToList(), 
            Genres = genres,
            Platforms = platforms, 
            Players = players
        };
        
        return View(catalogViewModel);
    }

    /// <summary>
    /// Фильтрация и поиск игр в каталоге
    /// </summary>
    [HttpPost]
    public IActionResult Index(string sortOrder, string searchString, CatalogViewModel model)
    {
        var selectedGenres = model.Genres.Where(v => v.Selected).Select(v => v.Name).ToList();
        if (selectedGenres.Count == 0) selectedGenres = _db.Genres.Select(v => v.Name).ToList();
        var selectedPlatforms = model.Platforms.Where(v => v.Selected).Select(v => v.Name).ToList();
        if (selectedPlatforms.Count == 0) selectedPlatforms = _db.Platforms.Select(v => v.Name).ToList();
        var selectedPlayers = model.Players.Where(v => v.Selected).Select(v => v.Name).ToList();
        if (selectedPlayers.Count == 0) selectedPlayers = _db.Players.Select(v => v.Name).ToList();

        // Сортировка и поиск игр
        var sortedSearchedGames = SortSearchGames(sortOrder, searchString).ToList();

        var games = new List<Game>();
        for (var i = 0; i < sortedSearchedGames.Count; i++)
        {
            var gameGenres = _db.Genres
                .Where(genre => genre.Games.Any(j => j.GameId == sortedSearchedGames[i].Id)).ToList();
            var gamePlatforms = _db.Platforms
                .Where(platform => platform.Games.Any(j => j.GameId == sortedSearchedGames[i].Id)).ToList();
            var gamePlayers = _db.Players
                .Where(player => player.Games.Any(j => j.GameId == sortedSearchedGames[i].Id)).ToList();
            
            games.AddRange(from t in gameGenres where selectedGenres.Contains(t.Name) select sortedSearchedGames[i]);
            foreach (var t in gamePlatforms.Where(t => !selectedPlatforms.Contains(t.Name))) games.Remove(sortedSearchedGames[i]);
            foreach (var t in gamePlayers.Where(t => !selectedPlayers.Contains(t.Name))) games.Remove(sortedSearchedGames[i]);
        }

        model.Games = games;
        model.Genres = model.Genres.ToList();
        model.Platforms = model.Platforms;
        model.Players = model.Players;

        return View(model);
    }

    /// <summary>
    /// Функция поиска игр
    /// </summary>
    private IEnumerable<Game> SortSearchGames(string sortOrder, string searchString)
    {
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // По названию
        ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date"; // По дате выхода
        ViewData["CurrentFilter"] = searchString;
        IQueryable<Game> games = _db.Games;

        // Поиск по названию игры
        if (!string.IsNullOrEmpty(searchString)) games = games.Where(s => s.Title.Contains(searchString));

        games = sortOrder switch
        {
            "name_desc" => games.OrderByDescending(s => s.Title),
            "Date" => games.OrderBy(s => s.ReleaseDate),
            "date_desc" => games.OrderByDescending(s => s.ReleaseDate),
            _ => games.OrderBy(s => s.Title)
        };

        return games;
    }
}