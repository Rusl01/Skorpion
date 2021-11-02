using System.Collections.Generic;
using System.Linq;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CatalogController : Controller
    {
        private ApplicationContext db;

        public CatalogController(ApplicationContext context)
        {
            db = context;
        }
        
        public IActionResult Index()
        {
            List<Game> games = db.Games.ToList();
            List<Genre> genres = db.Genres.ToList();
            GameViewModel gameViewModel = new GameViewModel { Games = games, Genres = genres };
            return View(gameViewModel);
        }
    }
}