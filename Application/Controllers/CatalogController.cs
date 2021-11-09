using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CatalogController : Controller
{
    private readonly ApplicationContext db;

    public CatalogController(ApplicationContext context)
    {
        db = context;
    }

    [HttpGet]
    public IActionResult Index(string sortOrder, string searchString)
    {
        var games = SortSearchGames(sortOrder, searchString);
        var genres = db.Genres.ToList();
        var platforms = db.Platforms.ToList();
        var players = db.Players.ToList();
        var catalogViewModel = new CatalogViewModel
            {Games = games.ToList(), Genres = genres, Platforms = platforms, Players = players};
        return View(catalogViewModel);
    }

    // Нужен рефакторинг
    [HttpPost]
    public IActionResult Index(string sortOrder, string searchString, CatalogViewModel catalogViewModel)
    {
        var selectedGenres = catalogViewModel.Genres.Where(v => v.Selected).Select(v => v.Name).ToList();
        if (selectedGenres.Count == 0) selectedGenres = db.Genres.Select(v => v.Name).ToList();
        var selectedPlatforms = catalogViewModel.Platforms.Where(v => v.Selected).Select(v => v.Name).ToList();
        if (selectedPlatforms.Count == 0) selectedPlatforms = db.Platforms.Select(v => v.Name).ToList();
        var selectedPlayers = catalogViewModel.Players.Where(v => v.Selected).Select(v => v.Name).ToList();
        if (selectedPlayers.Count == 0) selectedPlayers = db.Players.Select(v => v.Name).ToList();

        var games = SortSearchGames(sortOrder, searchString).ToList();

        var newGames = new List<Game>();
        for (var i = 0; i < games.Count; i++)
        {
            var gameGenres = db.Genres.Where(genre => genre.Games.Any(j => j.GameId == games[i].Id)).ToList();
            var gamePlatforms = db.Platforms.Where(platform => platform.Games.Any(j => j.GameId == games[i].Id))
                .ToList();
            var gamePlayers = db.Players.Where(player => player.Games.Any(j => j.GameId == games[i].Id)).ToList();
            newGames.AddRange(from t in gameGenres where selectedGenres.Contains(t.Name) select games[i]);
            foreach (var t in gamePlatforms.Where(t => !selectedPlatforms.Contains(t.Name))) newGames.Remove(games[i]);
            foreach (var t in gamePlayers.Where(t => !selectedPlayers.Contains(t.Name))) newGames.Remove(games[i]);
        }

        catalogViewModel.Games = newGames;
        catalogViewModel.Genres = catalogViewModel.Genres.ToList();
        catalogViewModel.Platforms = catalogViewModel.Platforms;
        catalogViewModel.Players = catalogViewModel.Players;

        return View(catalogViewModel);
    }

    private IEnumerable<Game> SortSearchGames(string sortOrder, string searchString)
    {
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        ViewData["CurrentFilter"] = searchString;
        IQueryable<Game> games = db.Games;

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