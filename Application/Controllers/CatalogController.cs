using System.Threading.Tasks;
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
    private const int PageSize = 6;

    public CatalogController(ApplicationContext context)
    {
        _db = context;
    }

    /// <summary>
    /// Страница каталога
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index(string sortOrder, string searchString, int page=1)
    {
        var games = SortSearchGames(sortOrder, searchString, _db.Games);
        var genres = _db.Genres.ToList();
        var platforms = _db.Platforms.ToList();
        var players = _db.Players.ToList();
        
        var count = games.Count();
        var items = games.Skip((page - 1) * PageSize).Take(PageSize).ToList();
        var pageViewModel = new PageViewModel(count, page, PageSize);
        
        var catalogViewModel = new CatalogViewModel
        {
            Games = items, 
            Genres = genres,
            Platforms = platforms, 
            Players = players,
            PageViewModel = pageViewModel
        };
        
        return View(catalogViewModel);
    }

    /// <summary>
    /// Фильтрация и поиск игр в каталоге
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Index(string sortOrder, string searchString, CatalogViewModel model, int page=1)
    {
        var selectedGenres = model.Genres.Where(v => v.Selected).ToList();
        if (selectedGenres.Count == 0) selectedGenres = _db.Genres.ToList();
        var selectedPlatforms = model.Platforms.Where(v => v.Selected).ToList();
        if (selectedPlatforms.Count == 0) selectedPlatforms = _db.Platforms.ToList();
        var selectedPlayers = model.Players.Where(v => v.Selected).ToList();
        if (selectedPlayers.Count == 0) selectedPlayers = _db.Players.ToList();

        // Отфильтровать по жанрам
        var games = _db.Games
            .Include(g => g.Genres)
            .Include(g => g.Platforms)
            .Include(g => g.Players).ToList();
        var gamesToDelete = new List<int>();
        foreach (var game in games)
        {
            var genresId = game.Genres.Select(x => x.GenreId).ToList();
            var selectedGenresId = selectedGenres.Select(x => x.Id);
            if (!genresId.Intersect(selectedGenresId).Any())
            {
                gamesToDelete.Add(game.Id);
            }
        }
        // Отфильтровать по платформам
        foreach (var game in games)
        {
            var platformsId = game.Platforms.Select(x => x.PlatformId).ToList();
            var selectedPlatformsId = selectedPlatforms.Select(x => x.Id);
            if (!platformsId.Intersect(selectedPlatformsId).Any())
            {
                gamesToDelete.Add(game.Id);
            }
        }
        
        // Отфильтровать по количеству игроков
        foreach (var game in games)
        {
            var playersId = game.Players.Select(x => x.PlayerId).ToList();
            var selectedPlayersId = selectedPlayers.Select(x => x.Id);
            if (!playersId.Intersect(selectedPlayersId).Any())
            {
                gamesToDelete.Add(game.Id);
            }
        }

        foreach (var id in gamesToDelete)
        {
            if (games.Select(x => x.Id).Contains(id))
            {
                games.Remove(_db.Games.First(g => g.Id == id));
            }
        }
        
        // Сортировка и поиск игр
        var sortedSearchedGames = SortSearchGames(sortOrder, searchString, games.AsQueryable()).ToList();
        
        var count = sortedSearchedGames.Count();
        var items = sortedSearchedGames.Skip((page - 1) * PageSize).Take(PageSize).ToList();
        var pageViewModel = new PageViewModel(count, page, PageSize);
        
        model.Games = items;
        model.Genres = model.Genres.ToList();
        model.Platforms = model.Platforms;
        model.Players = model.Players;
        model.PageViewModel = pageViewModel;

        return View(model);
    }

    /// <summary>
    /// Ссылка на разработчика
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Game(int id)
    {
        var game = await _db.Games.Include(g => g.Developer).FirstAsync(g => g.Id == id);
        return View(game);
    }

    /// <summary>
    /// Обработчик кнопки "На сайт разработчика"
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult ToDeveloperSite(string url)
    {
        return url[..8].Equals("https://") ? RedirectPermanent(url) : RedirectPermanent("https://" + url);
    }


    /// <summary>
    /// Функция поиска игр
    /// </summary>
    private IEnumerable<Game> SortSearchGames(string sortOrder, string searchString , IQueryable<Game> games)
    {
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // По названию
        ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date"; // По дате выхода
        ViewData["CurrentFilter"] = searchString;

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