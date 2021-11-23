using System.Threading.Tasks;
using Application.Data;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Key = Application.Models.Key;

namespace Application.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationContext db;
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        db = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var keyGames = new Dictionary<Guid, Game>();
        List<Key> keys = db.Keys.Where(x => x.UserId == user.Id).ToList();
        foreach (var key in keys)
        {
            Game game = db.Games.First(x => x.Id == key.GameId);
            keyGames.Add(key.Id, game);
        }
        return View(new ProfileViewModel{User = user, KeyGames = keyGames});
    }
    
    [HttpGet]
    [Authorize(Roles="admin")]
    public IActionResult Admin()
    {
        Console.WriteLine("Тут!!!");
        var users = _userManager.Users;
        Console.WriteLine("USERS Количество: " + users.Count());
        return View(users);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }
        return View(user);
    } 

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserPhotoUrl = model.UserPhotoUrl,
                Nickname = model.Nickname,
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        }
        return View(model);
    }
    
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Login");
    }
    
    public async Task<IActionResult> Remove(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return RedirectToAction("Admin");
    }
}