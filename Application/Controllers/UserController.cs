using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

public class UserController : Controller
{
    private ApplicationContext db;

    public UserController(ApplicationContext context)
    {
        db = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

    public IActionResult SignIn()
    {
        return View();
    }
}