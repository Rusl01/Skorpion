using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    public class CartController : Controller
    {
        private ApplicationContext db;

        public CartController(ApplicationContext context)
        {
            db = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}