using System.Security.Claims;
using System.Threading.Tasks;
using Application.Data;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

/// <summary>
/// Сервис для работы с авторизованными пользователями
/// </summary>
public class UserService : IUserService
{
    private readonly ApplicationContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor, ApplicationContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _db = context;
    }

    /// <summary>
    /// Метод для получения текущего авторизованного пользователя
    /// </summary>
    public async Task<User> getCurrentUserAsync()
    {
        var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        var user = await _db.Users.FirstAsync(user => user.UserName == userName);
        return user;
    }

    /// <summary>
    /// Метод для получения пользователя по id
    /// </summary>
    public async Task<User> getUserByUserNameAsync(string userName)
    {
        var user = await _db.Users.FirstAsync(user => user.UserName == userName);
        return user;
    }
    
    /// <summary>
    /// Метод для получения игр пользователя
    /// </summary>
    public ICollection<Game> getUserGames(User user)
    {
        var keys = _db.Keys.Where(x => x.UserId == user.Id).ToList();
        var games = keys.Select(key => _db.Games.First(x => x.Id == key.GameId)).ToList();
        return games;
    }
    
    /// <summary>
    /// Метод для получения друзей пользователя
    /// </summary>
    public ICollection<User> getUserFriends(User user)
    {
        var friendIds = _db.Friends
            .Where(x => x.FirstUserId == user.Id || x.SecondUserId == user.Id).ToList()
            .Select(friend => friend.FirstUserId == user.Id ? friend.SecondUserId : friend.FirstUserId).ToList();
        var friends = friendIds.Select(friendId => _db.Users.First(x => x.Id == friendId)).ToList();
        return friends;
    }

    /// <summary>
    /// Метод для получения ключей от купленных игр
    /// </summary>
    public async Task<Dictionary<Guid, Game>> getCurrentUserKeysAsync()
    {
        var currentUser = await getCurrentUserAsync();
        var keys = _db.Keys.Where(key => key.UserId == currentUser.Id).ToList();
        return keys.ToDictionary(key => key.Id, key => _db.Games.First(g => g.Id == key.GameId));
    }

    /// <summary>
    /// Метод для удаления пользователя
    /// </summary>
    public async void DeleteUser(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Метод для добавления пользователя в список друзей авторизованного пользователя
    /// </summary>
    public async void AddUserToFriendList(User user)
    {
        var currentUser = await getCurrentUserAsync();

        if (!IsFriend(currentUser, user))
        {
            await _db.Friends.AddAsync(new Friend
            {
                FirstUserId = currentUser.Id,
                SecondUserId = user.Id,
                FirstUser = currentUser,
                SecondUser = user
            });
            await _db.SaveChangesAsync();
        }
    }
    
    /// <summary>
    /// Метод для добавления пользователя в список друзей авторизованного пользователя
    /// </summary>
    public async void RemoveUserFromFriendList(User user)
    {
        var currentUser = await getCurrentUserAsync();
        _db.Friends.Remove(_db.Friends.First(x =>
            x.FirstUserId == user.Id && x.SecondUserId == currentUser.Id ||
            x.SecondUserId == user.Id && x.FirstUserId == currentUser.Id));
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Метод для проверки дружбы между двумя пользователями
    /// </summary>
    public bool IsFriend(User firstUser, User secondUser)
    {
        var friendsList = _db.Friends.ToList();
        var idList = new List<string>();
        foreach (var friends in friendsList)
        {
            idList.Add(friends.FirstUserId);
            idList.Add(friends.SecondUserId);
            if (idList.Contains(firstUser.Id) && idList.Contains(secondUser.Id))
            {
                return true;
            }

            idList.Clear();
        }

        return false;
    }
}