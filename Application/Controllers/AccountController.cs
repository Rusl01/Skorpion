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
    private readonly ApplicationContext _db;
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = context;
    }
    
    public async Task<IActionResult> Index(string id)
    {
        Console.WriteLine("Usernmae User: ", id);
        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == id);
        
        var keyGames = new Dictionary<Guid, Game>();
        var keys = _db.Keys.Where(x => x.UserId == user.Id).ToList();
        foreach (var key in keys)
        {
            var game = _db.Games.First(x => x.Id == key.GameId);
            keyGames.Add(key.Id, game);
        }
        
        var addFriend = user.Id != _userManager.GetUserAsync(HttpContext.User).Result.Id;
        
        var friendIds = _db.Friends
            .Where(x => x.FirstUserId == user.Id || x.SecondUserId == user.Id).ToList()
            .Select(friend => friend.FirstUserId == user.Id ? friend.SecondUserId : friend.FirstUserId).ToList();
        var friends = friendIds.Select(friendId => _db.Users.First(x => x.Id == friendId)).ToList();
        return View(new ProfileViewModel{User = user, KeyGames = keyGames, AddFriend = addFriend, Friends = friends});
    }
    
    [HttpGet]
    [Authorize(Roles="admin")]
    public IActionResult Admin()
    {
        var users = _userManager.Users;
        return View(users);
    }

    [HttpGet]
    public IActionResult FindUser(FindUserViewModel model)
    {
        var users = _db.Users.Where(x => x.Nickname == model.Nickname).ToList();
        return View(new FindUserViewModel {Users = users, Nickname = model.Nickname});
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
    
    public async Task<IActionResult> AddFriend(string id)
    {
        Console.WriteLine("AddFriend");
        await _db.Friends.AddAsync(new Friend
        {
            FirstUserId = _userManager.GetUserAsync(HttpContext.User).Result.Id,
            SecondUserId = _db.Users.First(x => x.UserName == id).Id,
            FirstUser = _userManager.GetUserAsync(HttpContext.User).Result,
            SecondUser = _db.Users.First(x => x.UserName == id)
        });
        await _db.SaveChangesAsync();
        Console.WriteLine("SavesChanges");
        return RedirectToAction("Index", new { id });
    }
    
    public async Task<IActionResult> Remove(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return RedirectToAction("Admin");
    }
}