using System.Threading.Tasks;
using Application.Data;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

/// <summary>
/// Контроллер для управления аутентификацией, авторизацией и регистрацией
/// </summary>
[Authorize]
public class AccountController : Controller
{
    private readonly ApplicationContext _db;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private const int PageSize = 10;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
        ApplicationContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = context;
    }

    /// <summary>
    /// Страница профиля
    /// </summary>
    public async Task<IActionResult> Index(string id)
    {
        var userName = id;
        Console.WriteLine("userName: " + userName);
        var user = await _db.Users.FirstAsync(x => x.UserName == userName);
        var keys = _db.Keys.Where(x => x.UserId == user.Id).ToList();
        var games = keys.Select(key => _db.Games.First(x => x.Id == key.GameId)).ToList();

        var friendIds = _db.Friends
            .Where(x => x.FirstUserId == user.Id || x.SecondUserId == user.Id).ToList()
            .Select(friend => friend.FirstUserId == user.Id ? friend.SecondUserId : friend.FirstUserId).ToList();
        var friends = friendIds.Select(friendId => _db.Users.First(x => x.Id == friendId)).ToList();

        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var addFriend = !isFriend(currentUser.Id, user.Id);
        Console.WriteLine("addFriend: " + addFriend);
        
        return View(new ProfileViewModel {User = user, Games = games, AddFriend = addFriend, Friends = friends});
    }

    public bool isFriend(string currentUserId, string userId)
    {
        var friendsList = _db.Friends.ToList();
        var idList = new List<string>();
        foreach (var friends in friendsList)
        {
            idList.Add(friends.FirstUserId);
            idList.Add(friends.SecondUserId);
            if (idList.Contains(currentUserId) && idList.Contains(userId))
            {
                return true;
            }
            idList.Clear();
        }
        return false;
    }

    /// <summary>
    /// Страница списка всех пользователей
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "admin")]
    public IActionResult Admin()
    {
        var users = _userManager.Users;
        return View(users);
    }

    /// <summary>
    /// Страница поиска пользователя
    /// </summary>
    [HttpGet]
    public IActionResult FindUser(FindUserViewModel model, int page=1)
    {
        List<User> users;
        if (model.Nickname != null)
        {
            Console.WriteLine("model:" + model.Nickname.Length + "!");
        }
        else
        {
            Console.WriteLine("Fail null object");
            model.Nickname = new string("");
        }
        users = !string.IsNullOrEmpty(model.Nickname) ? 
            _db.Users.Where(x => x.Nickname == model.Nickname).ToList() : _db.Users.ToList();
        
        var count = users.Count();
        var items = users.Skip((page - 1) * PageSize).Take(PageSize).ToList();
        var pageViewModel = new PageViewModel(count, page, PageSize);
        
        var newModel = new FindUserViewModel
        {
            Users = items, 
            Nickname = model.Nickname, 
            PageViewModel = pageViewModel
        };
        
        return View(newModel);
    }

    /// <summary>
    /// Страница регистрации
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }
    
    /// <summary>
    /// Страница редактирования пользователя
    /// </summary>
    [HttpGet]
    public IActionResult Update()
    {
        var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
        var model = new UpdateViewModel
        {
            UserPhotoUrl = currentUser.UserPhotoUrl,
            Nickname = currentUser.Nickname,
            Email = currentUser.Email
        };
        return View(model);
    }

    /// <summary>
    /// Страница авторизации
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    
    /// <summary>
    /// Страница библиотеки игр пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Library(int page=1)
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var keys = _db.Keys.Where(key => key.UserId == currentUser.Id).ToList();
        Dictionary<Guid, Game> keyGames = new Dictionary<Guid, Game>();
        foreach (var key in keys)
        {
            keyGames.Add(key.Id, _db.Games.First(g => g.Id == key.GameId));
        }
        
        var count = keyGames.Count();
        var items = keyGames.Skip((page - 1) * PageSize).Take(PageSize).ToList();
        var pageViewModel = new PageViewModel(count, page, PageSize);
        
        var model = new LibraryViewModel
        {
            KeyGames = keyGames,
            PageViewModel = pageViewModel
        };
        return View(model);
    }

    /// <summary>
    /// Авторизация пользователя в системе
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel user)
    {
        if (!ModelState.IsValid) return View(user);
        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
        if (result.Succeeded) return RedirectToAction("Index", "Home");
        ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
        return View(user);
    }

    /// <summary>
    /// Регистрация пользователя в базе данных
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = new User
        {
            UserPhotoUrl = model.UserPhotoUrl,
            Nickname = model.Nickname,
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("index", "Home");
        }

        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);

        ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");

        return View(model);
    }
    
    /// <summary>
    /// Редактирование пользователя
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Update(UpdateViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = _userManager.GetUserAsync(HttpContext.User).Result;
        user.UserPhotoUrl = model.UserPhotoUrl;
        user.Nickname = model.Nickname;
        user.UserName = model.Email;
        user.Email = model.Email;
            
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);

        ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");

        return View(model);
    }

    /// <summary>
    /// Разлогинирование пользователя
    /// </summary>
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Login");
    }

    /// <summary>
    /// Добавить пользователя с id к текущему пользователю в друзья
    /// </summary>
    public async Task<IActionResult> AddFriend(string id)
    {
        var friendsList = _db.Friends.ToList();
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var user = _db.Users.First(x => x.Id == id);

        var idList = new List<string>();
        foreach (var friends in friendsList)
        {
            idList.Add(friends.FirstUserId);
            idList.Add(friends.SecondUserId);
            if (idList.Contains(currentUser.Id) && idList.Contains(user.Id))
            {
                return RedirectToAction("Index", new { id = user.UserName });
            }
            idList.Clear();
        }

        await _db.Friends.AddAsync(new Friend
        {
            FirstUserId = currentUser.Id,
            SecondUserId = user.Id,
            FirstUser = currentUser,
            SecondUser = user
        });
        await _db.SaveChangesAsync();

        return RedirectToAction("Index", new { id = user.UserName });
    }

    /// <summary>
    /// Удаление пользователя с id из списка друзей текущего пользователя
    /// </summary>
    public async Task<IActionResult> RemoveFriend(string id)
    {
        var currentUserId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
        var friend = _db.Users.First(x => x.Id == id);
        _db.Friends.Remove(_db.Friends.First(x =>
            x.FirstUserId == friend.Id && x.SecondUserId == currentUserId ||
            x.SecondUserId == friend.Id && x.FirstUserId == currentUserId));
        await _db.SaveChangesAsync();
        
        return RedirectToAction("Index", new { id = friend.UserName });
    }

    /// <summary>
    /// Удаление пользователя из базы данных
    /// </summary>
    public async Task<IActionResult> Remove(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        
        return RedirectToAction("Admin");
    }
}