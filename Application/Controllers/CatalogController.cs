using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers
{
    public class CatalogController : Controller
    {
        private ApplicationContext db;

        public CatalogController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            IQueryable<Game> games = db.Games;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    games = games.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    games = games.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    games = games.OrderBy(s => s.Title);
                    break;
            }

            var genres = db.Genres.ToList();
            List<Platform> platforms = db.Platforms.ToList();
            List<Player> players = db.Players.ToList();
            var catalogViewModel = new CatalogViewModel {Games = games.ToList(), Genres = genres, Platforms = platforms, Players = players };
            return View(catalogViewModel);
        }

        [HttpPost]
        public IActionResult Index(CatalogViewModel catalogViewModel)
        {
            Console.WriteLine("Выбранные жанры:");
            var selectedGenres = new List<string>();
            foreach (var v in catalogViewModel.Genres)
            {
                Console.WriteLine(v.Name + " " + v.Selected);
                if (v.Selected)
                {
                    selectedGenres.Add(v.Name);
                }
            }
            var games = db.Games.ToList();
            var newGames = new List<Game>();
            for (var i = 0; i < games.Count; i++)
            {
                var gameGenres = db.Genres
                    .Where(genre => genre.Games
                        .Any(j => j.GameId == games[i].Id)).ToList();
                for (var j = 0; j < gameGenres.Count; j++)
                {
                    if (selectedGenres.Contains(gameGenres[j].Name))
                    {
                        newGames.Add(games[i]);
                    }
                }
            }
            catalogViewModel.Games = newGames;
            catalogViewModel.Genres = catalogViewModel.Genres.ToList();
            catalogViewModel.Platforms = catalogViewModel.Platforms;
            catalogViewModel.Players = catalogViewModel.Players;
            Console.WriteLine("Выбранные игры!!!");
            foreach (var v in catalogViewModel.Games)
            {
                Console.WriteLine(v.Title);
            }

            return View(catalogViewModel);
        }
    }
}