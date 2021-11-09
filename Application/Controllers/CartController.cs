using Application.Helpers;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class CartController : Controller
{
    private readonly ApplicationContext db;

    public CartController(ApplicationContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
        if (cart != null)
        {
            ViewBag.cart = cart;
            ViewBag.total = cart.Aggregate(0.0, (current, item) => current + item.Game.Price);
        }
        else
        {
            ViewBag.cart = new List<Item>();
            ViewBag.total = 0.0;
        }

        Console.WriteLine("Cart Index");
        return View();
    }

    public IActionResult Buy(string id)
    {
        Console.WriteLine("ID!" + id);
        var product = db.Games.FirstOrDefault(x => x.Id == int.Parse(id));
        Console.WriteLine(product);
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

        return RedirectToAction("Index");
    }

    public IActionResult Remove(string id)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
        for (var i = 0; i < cart.Count; i++)
            if (cart[i].Game.Id.Equals(int.Parse(id)))
                cart.RemoveAt(i);
        HttpContext.Session.SetObjectAsJson("cart", cart);
        return RedirectToAction("Index");
    }

    private int IsExist(string id)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
        for (var i = 0; i < cart.Count; i++)
            if (cart[i].Game.Id.Equals(int.Parse(id)))
                return i;

        return -1;
    }
}