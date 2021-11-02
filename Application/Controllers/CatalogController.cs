using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            IQueryable<Game> games = db.Games;
            foreach (var game in games)
            {
                Console.WriteLine(game.Title);
            }
            Console.WriteLine("Конец");
            Console.WriteLine(searchString);
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.Contains(searchString));
                foreach (var game in games)
                {
                    Console.WriteLine(game.Title);
                }
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
            
            List<Genre> genres = db.Genres.ToList();
            GameViewModel gameViewModel = new GameViewModel {Games = games.ToList(), Genres = genres};
            return View(gameViewModel);
        }
    }
}