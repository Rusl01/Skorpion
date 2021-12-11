using System.Threading.Tasks;
using Application.Models;

namespace Application.Services;

/// <summary>
/// Интерфейс сервиса для работы с корзиной
/// </summary>
public interface IUserService
{
    Task<User> getCurrentUserAsync();
    Task<User> getUserByUserNameAsync(string userName);
    ICollection<Game> getUserGames(User user);
    ICollection<User> getUserFriends(User user);
    Task<Dictionary<Guid, Game>> getCurrentUserKeysAsync();
    void DeleteUser(User user);
    void AddUserToFriendList(User user);
    void RemoveUserFromFriendList(User user);
    bool IsFriend(User firstUser, User secondUser);
}