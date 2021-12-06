﻿using System.Threading.Tasks;
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
        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == id);

        var keyGames = new Dictionary<Guid, Game>();
        var keys = _db.Keys.Where(x => x.UserId == user.Id).ToList();
        foreach (var key in keys)
        {
            var game = _db.Games.First(x => x.Id == key.GameId);
            keyGames.Add(key.Id, game);
        }

        var friendIds = _db.Friends
            .Where(x => x.FirstUserId == user.Id || x.SecondUserId == user.Id).ToList()
            .Select(friend => friend.FirstUserId == user.Id ? friend.SecondUserId : friend.FirstUserId).ToList();
        var friends = friendIds.Select(friendId => _db.Users.First(x => x.Id == friendId)).ToList();

        var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
        var currentUserFriendIds = _db.Friends
            .Where(x => x.FirstUserId == currentUser.Id || x.SecondUserId == currentUser.Id).ToList()
            .Select(friend => friend.FirstUserId == currentUser.Id ? friend.SecondUserId : friend.FirstUserId).ToList();
        var currentUserFriends = friendIds.Select(friendId => _db.Users.First(x => x.Id == friendId)).ToList();
        var addFriend = false;
        if (user != null) addFriend = !currentUserFriends.Contains(_db.Users.First(x => x.UserName == id));


        return View(new ProfileViewModel {User = user, KeyGames = keyGames, AddFriend = addFriend, Friends = friends});
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
    public IActionResult FindUser(FindUserViewModel model)
    {
        var users = _db.Users.Where(x => x.Nickname == model.Nickname).ToList();
        return View(new FindUserViewModel {Users = users, Nickname = model.Nickname});
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
            UserPhotoUrl = currentUser.UserPhotoUrl
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
        await _db.Friends.AddAsync(new Friend
        {
            FirstUserId = _userManager.GetUserAsync(HttpContext.User).Result.Id,
            SecondUserId = _db.Users.First(x => x.UserName == id).Id,
            FirstUser = _userManager.GetUserAsync(HttpContext.User).Result,
            SecondUser = _db.Users.First(x => x.UserName == id)
        });
        await _db.SaveChangesAsync();
        
        return RedirectToAction("Index", new {id});
    }

    /// <summary>
    /// Удаление пользователя с id из списка друзей текущего пользователя
    /// </summary>
    public async Task<IActionResult> RemoveFriend(string id)
    {
        var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
        var friend = _db.Users.First(x => x.UserName == id);
        _db.Friends.Remove(_db.Friends.First(x =>
            x.FirstUserId == friend.Id && x.SecondUserId == userId ||
            x.SecondUserId == friend.Id && x.FirstUserId == userId));
        await _db.SaveChangesAsync();
        
        return RedirectToAction("Index", new {id});
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