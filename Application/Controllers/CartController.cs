using System.Threading.Tasks;
using Application.Data;
using Application.Helpers;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Контроллер для управления корзиной
/// </summary>
public class CartController : Controller
{
    private readonly ApplicationContext _db;
    private readonly UserManager<User> _userManager;

    public CartController(ApplicationContext context, UserManager<User> userManager)
    {
        _db = context;
        _userManager = userManager;
    }

    /// <summary>
    /// Страница оформления заказа
    /// </summary>
    public IActionResult Checkout()
    {
        //Можно убрать лишние ветвления
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Aggregate(0.0, (current, item) => current + item.Game.Price);
                ViewBag.cook = 0;
            }
            else
            {
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0.0;
                ViewBag.cook = 0;
            }
        }
        else
        {
            User currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            List<CartItem> cart = _db.CartItems.Where(c => c.UserId == currentUser.Id).ToList();
            List<Game> cartGames = new List<Game>();
            for (int i = 0; i < cart.Count(); i++)
            {
                cartGames.Add(_db.Games.First(x => x.Id.ToString() == cart[i].GameId));
            }

            if (cartGames.Any())
            {
                Console.WriteLine("Корзина не пуста");
                ViewBag.cart = cartGames;
                ViewBag.total = cartGames.Aggregate(0.0, (current, item) => current + item.Price);
                ViewBag.cook = 1;
            }
            else
            {
                Console.WriteLine("Корзина пуста");
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0.0;
                ViewBag.cook = 1;
            }
        }

        return View();
    }

    /// <summary>
    /// Закончить оформление заказа
    /// </summary>
    [HttpPost]
    public IActionResult Checkout(CheckoutViewModel model)
    {
        return RedirectToAction("Key");
    }

    /// <summary>
    /// Страница выдачи ключей от игр
    /// </summary>
    public async Task<IActionResult> Key()
    {
        var keys = new Dictionary<string, string>();
        //Можно убрать лишние ветвления
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Aggregate(0.0, (current, item) => current + item.Game.Price);
                ViewBag.cook = 0;
                
                foreach (var item in ViewBag.cart)
                {
                    Guid token = Guid.NewGuid();
                    _db.Keys.Add(new Key {Id = token, GameId = item.Game.Id, UserId = null});
                    var id = item.Game.Id;
                    var gamesAll = _db.Games;
                    foreach (var game in gamesAll)
                    {
                        if (game.Id == id)
                        {
                            keys.Add(token.ToString(), game.DeveloperSite);
                        }
                    }
                }
                _db.SaveChanges();
            }
            else
            {
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0.0;
                ViewBag.cook = 0;
            }
        }
        else
        {
            User currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            List<CartItem> cart = _db.CartItems.Where(c => c.UserId == currentUser.Id).ToList();
            List<Game> cartGames = new List<Game>();
            for (int i = 0; i < cart.Count(); i++)
            {
                cartGames.Add(_db.Games.First(x => x.Id.ToString() == cart[i].GameId));
            }

            if (cartGames.Any())
            {
                Console.WriteLine("Корзина не пуста");
                ViewBag.cart = cartGames;
                ViewBag.total = cartGames.Aggregate(0.0, (current, item) => current + item.Price);
                ViewBag.cook = 1;
                foreach (var item in ViewBag.cart)
                {
                    Guid token = Guid.NewGuid();
                    _db.Keys.Add(new Key {Id = token, GameId = item.Id, UserId = currentUser.Id});
                    var id = item.Id;
                    var gamesAll = _db.Games;
                    foreach (var game in gamesAll)
                    {
                        if (game.Id == id)
                        {
                            keys.Add(token.ToString(), game.DeveloperSite);
                        }
                    }
                }
                _db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Корзина пуста");
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0.0;
                ViewBag.cook = 1;
            }
        }
        
        // Очищаем корзину
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            var cart = new List<Item>();
            HttpContext.Session.SetObjectAsJson("cart", cart);
        }
        else
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            var cartItems = _db.CartItems.Where(u => u.UserId == currentUser.Id);
            _db.CartItems.RemoveRange(cartItems);
            await _db.SaveChangesAsync();
        }

        var model = new KeyViewModel
        {
            KeySite = keys
        };
        
        return View(model);
    }

    /// <summary>
    /// Страница корзины
    /// </summary>
    [AllowAnonymous]
    public IActionResult Index()
    {
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Aggregate(0.0, (current, item) => current + item.Game.Price);
                ViewBag.cook = 0;
            }
            else
            {
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0.0;
                ViewBag.cook = 0;
            }
        }
        else
        {
            User currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            List<CartItem> cart = _db.CartItems.Where(c => c.UserId == currentUser.Id).ToList();
            List<Game> cartGames = new List<Game>();
            for (int i = 0; i < cart.Count(); i++)
            {
                cartGames.Add(_db.Games.First(x => x.Id.ToString() == cart[i].GameId));
            }
            if (cartGames.Any())
            {
                Console.WriteLine("Корзина не пуста");
                ViewBag.cart = cartGames;
                ViewBag.total = cartGames.Aggregate(0.0, (current, item) => current + item.Price);
                ViewBag.cook = 1;
            }
            else
            {
                Console.WriteLine("Корзина пуста");
                ViewBag.cart = new List<Item>();
                ViewBag.total = 0.0;
                ViewBag.cook = 1;
            }
        }
        Console.WriteLine("Cart Index");
        return View();
    }

    /// <summary>
    /// Добавление игры в корзину
    /// </summary>
    public IActionResult Buy(string id)
    {
        Game product = _db.Games.First(x => x.Id == int.Parse(id));
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            if (HttpContext.Session.GetObjectFromJson<List<Item>>("cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item {Game = product});
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                var index = IsExist(id);
                if (index == -1) cart.Add(new Item {Game = product});
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
        }
        else
        {
            User currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            List<CartItem> cart = _db.CartItems.Where(c => c.UserId == currentUser.Id).ToList();
            List<Game> cartGames = new List<Game>();
            for (int i = 0; i < cart.Count(); i++)
            {
                cartGames.Add(_db.Games.First(x => x.Id.ToString() == cart[i].GameId));
            }

            if (!cartGames.Any())
            {
                _db.CartItems.Add(new CartItem {UserId = currentUser.Id, GameId = product.Id.ToString()});
            }
            else
            {
                var index = IsExist(id);
                if (index == -1)
                    _db.CartItems.Add(new CartItem {UserId = currentUser.Id, GameId = product.Id.ToString()});
            }
            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Удаление игры из корзины
    /// </summary>
    public IActionResult Remove(string id)
    {
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            for (var i = 0; i < cart.Count; i++)
                if (cart[i].Game.Id.Equals(int.Parse(id)))
                    cart.RemoveAt(i);
            HttpContext.Session.SetObjectAsJson("cart", cart);
        }
        else
        {
            User currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            CartItem cartitem = _db.CartItems.Where(u => u.UserId == currentUser.Id).First(c => c.GameId == id);
            _db.CartItems.Remove(cartitem);
            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Функция проверяет есть ли игра с данным id в корзине
    /// </summary>
    private int IsExist(string id)
    {
        if ((User.Identity != null && !User.Identity.IsAuthenticated) || User.Identity == null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            for (var i = 0; i < cart.Count; i++)
                if (cart[i].Game.Id.Equals(int.Parse(id)))
                    return i;
        }
        else
        {
            User currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            List<CartItem> cart = _db.CartItems.Where(c => c.UserId == currentUser.Id).ToList();
            List<Game> cartGames = new List<Game>();
            for (int i = 0; i < cart.Count(); i++)
            {
                cartGames.Add(_db.Games.First(x => x.Id.ToString() == cart[i].GameId));
            }

            for (int i = 0; i < cartGames.Count(); i++)
            {
                if (cartGames[i].Id.ToString() == id) return cartGames[i].Id;
            }
        }

        return -1;
    }
}