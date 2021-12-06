using Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Application.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<GameGenre> GameGenres { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }
    public DbSet<GamePlayer> GamePlayers { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Key> Keys { get; set; }
    public DbSet<Friend> Friends { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GameGenre>().HasKey(i => new {i.GameId, i.GenreId});
        builder.Entity<GamePlatform>().HasKey(i => new {i.GameId, i.PlatformId});
        builder.Entity<GamePlayer>().HasKey(i => new {i.GameId, i.PlayerId});
        builder.Entity<Friend>().HasKey(i => new {i.FirstUserId, i.SecondUserId});
        
        builder.Entity<Genre>().HasData(
            new Genre { Id=1, Name="RPG" }, 
            new Genre { Id=2, Name="Action Real Time Strategy" }, 
            new Genre { Id=3, Name="Racing" }, 
            new Genre { Id=4, Name="Platform" }, 
            new Genre { Id=5, Name="Strategy" }, 
            new Genre { Id=6, Name="Action" }
        );
        
        builder.Entity<Platform>().HasData(
            new Platform { Id=1, Name="Windows" }, 
            new Platform { Id=2, Name="Linux" }, 
            new Platform { Id=3, Name="Mac" }
        );
        
        builder.Entity<Player>().HasData(
            new Player { Id=1, Name="Одиночная игра" }, 
            new Player { Id=2, Name="Кооперативная игра" }, 
            new Player { Id=3, Name="Многопользовательская игра" }
        );
        
        builder.Entity<User>().HasData(
            new User
            {
                Id ="38ba2210-9dd4-438d-87ad-e2e760b1d82f", 
                Nickname = "taiiiga", 
                UserPhotoUrl = "https://sun9-3.userapi.com/impg/m0NBFMbT4dHGZ4T8e1USS-10CNvqRXK2t3tEAA/-futd1w-5Fw.jpg?size=608x606&quality=96&sign=948707fd6519ed8a10a1522e33d72180&type=album", 
                UserName = "blankface.vs.death@gmail.com", 
                NormalizedUserName = "BLANKFACE.VS.DEATH@GMAIL.COM", 
                Email = "blankface.vs.death@gmail.com", 
                NormalizedEmail = "BLANKFACE.VS.DEATH@GMAIL.COM", 
                EmailConfirmed = false, 
                PasswordHash = "AQAAAAEAACcQAAAAEIwFqQBeBsO+P9egSBNLzUmX19b2/JLNqkfJvRDBjbZ4r0HTD8A+isUyEL2/jkhDug==", 
                SecurityStamp = "5EKW2LF5H7W2FAGPC3YBPFC4OR7OASKI", 
                ConcurrencyStamp = "0089f406-6cea-4e2f-809b-5edc6c457cf9", 
                PhoneNumberConfirmed = false, 
                TwoFactorEnabled = false, 
                LockoutEnabled = true, 
                AccessFailedCount = 0
            },
            new User
            {
                Id = "5913fe8a-d3c0-45d7-8eb0-c67e832e96b1", 
                Nickname = "Chelovekov", 
                UserPhotoUrl = "https://sun9-3.userapi.com/impg/m0NBFMbT4dHGZ4T8e1USS-10CNvqRXK2t3tEAA/-futd1w-5Fw.jpg?size=608x606&quality=96&sign=948707fd6519ed8a10a1522e33d72180&type=album", 
                UserName = "chel@gmail.com", 
                NormalizedUserName = "CHEL@GMAIL.COM", 
                Email = "chel@gmail.com", 
                NormalizedEmail = "CHEL@GMAIL.COM", 
                EmailConfirmed = false, 
                PasswordHash = "AQAAAAEAACcQAAAAEDB6QAg7UfiyEfI5UolqF4xTsM5uhHHnTBYDSHgAxIsTVf3sLljdNyWtX/WcSGwOsA==", 
                SecurityStamp = "GWKUR75VALZRPPZ2DME52BTYCJ3KHOM5", 
                ConcurrencyStamp = "8a5d224e-599d-4931-9e61-78a370a42640", 
                PhoneNumberConfirmed = false, 
                TwoFactorEnabled = false, 
                LockoutEnabled = true, 
                AccessFailedCount = 0
            }
        );

        builder.Entity<Game>().HasData(
            new Game { Id=1, Title="Инди игра №1", ReleaseDate = new DateTime(2011,10,21), Description = "Описание для игры №1", Cover = "https://catherineasquithgallery.com/uploads/posts/2021-02/1614325996_3-p-raznotsvetnie-kvadrati-fon-3.jpg", Price = 1000, Developer = Users.ToList()[0] }, 
            new Game { Id=2, Title="Инди игра №2", ReleaseDate = new DateTime(2011,10,5), Description = "Описание для игры №2", Cover = "https://catherineasquithgallery.com/uploads/posts/2021-02/1614325996_3-p-raznotsvetnie-kvadrati-fon-3.jpg", Price = 2000, Developer = Users.ToList()[0] }, 
            new Game { Id=3, Title="Инди игра №3", ReleaseDate = new DateTime(2011,10,2), Description = "Описание для игры №3", Cover = "https://catherineasquithgallery.com/uploads/posts/2021-02/1614325996_3-p-raznotsvetnie-kvadrati-fon-3.jpg", Price = 500, Developer = Users.ToList()[0] },
            new Game { Id=4, Title="Инди игра №4", ReleaseDate = new DateTime(2011,10,7), Description = "Описание для игры №4", Cover = "https://catherineasquithgallery.com/uploads/posts/2021-02/1614325996_3-p-raznotsvetnie-kvadrati-fon-3.jpg", Price = 300, Developer = Users.ToList()[0] }, 
            new Game { Id=5, Title="Инди игра №5", ReleaseDate = new DateTime(2011,10,25), Description = "Описание для игры №5", Cover = "https://catherineasquithgallery.com/uploads/posts/2021-02/1614325996_3-p-raznotsvetnie-kvadrati-fon-3.jpg", Price = 200, Developer = Users.ToList()[0] }, 
            new Game { Id=6, Title="Инди игра №6", ReleaseDate = new DateTime(2011,10,15), Description = "Описание для игры №6", Cover = "https://catherineasquithgallery.com/uploads/posts/2021-02/1614325996_3-p-raznotsvetnie-kvadrati-fon-3.jpg", Price = 150, Developer = Users.ToList()[0] }
        );
        
        builder.Entity<GameGenre>().HasData(
            new GameGenre { GameId = 1, Game = Games.First(g => g.Id == 1), GenreId = 1, Genre = Genres.First(g => g.Id == 1) },
            new GameGenre { GameId = 2, Game = Games.First(g => g.Id == 2), GenreId = 2, Genre = Genres.First(g => g.Id == 2) },
            new GameGenre { GameId = 3, Game = Games.First(g => g.Id == 3), GenreId = 3, Genre = Genres.First(g => g.Id == 3) },
            new GameGenre { GameId = 4, Game = Games.First(g => g.Id == 4), GenreId = 4, Genre = Genres.First(g => g.Id == 4) },
            new GameGenre { GameId = 5, Game = Games.First(g => g.Id == 5), GenreId = 5, Genre = Genres.First(g => g.Id == 5) },
            new GameGenre { GameId = 6, Game = Games.First(g => g.Id == 6), GenreId = 6, Genre = Genres.First(g => g.Id == 6) }
        );
        
        builder.Entity<GamePlatform>().HasData(
            new GamePlatform { GameId = 1, Game = Games.First(g => g.Id == 1), PlatformId = 1, Platform = Platforms.First(p => p.Id == 1) },
            new GamePlatform { GameId = 2, Game = Games.First(g => g.Id == 2), PlatformId = 2, Platform = Platforms.First(p => p.Id == 2) },
            new GamePlatform { GameId = 3, Game = Games.First(g => g.Id == 3), PlatformId = 3, Platform = Platforms.First(p => p.Id == 3) },
            new GamePlatform { GameId = 4, Game = Games.First(g => g.Id == 4), PlatformId = 1, Platform = Platforms.First(p => p.Id == 1) },
            new GamePlatform { GameId = 5, Game = Games.First(g => g.Id == 5), PlatformId = 2, Platform = Platforms.First(p => p.Id == 2) },
            new GamePlatform { GameId = 6, Game = Games.First(g => g.Id == 6), PlatformId = 3, Platform = Platforms.First(p => p.Id == 3) }
        );
        
        builder.Entity<GamePlayer>().HasData(
            new GamePlayer { GameId = 1, Game = Games.First(g => g.Id == 1), PlayerId = 1, Player = Players.First(p => p.Id == 1) },
            new GamePlayer { GameId = 2, Game = Games.First(g => g.Id == 2), PlayerId = 2, Player = Players.First(p => p.Id == 2) },
            new GamePlayer { GameId = 3, Game = Games.First(g => g.Id == 3), PlayerId = 3, Player = Players.First(p => p.Id == 3) },
            new GamePlayer { GameId = 4, Game = Games.First(g => g.Id == 4), PlayerId = 1, Player = Players.First(p => p.Id == 1) },
            new GamePlayer { GameId = 5, Game = Games.First(g => g.Id == 5), PlayerId = 2, Player = Players.First(p => p.Id == 2) },
            new GamePlayer { GameId = 6, Game = Games.First(g => g.Id == 6), PlayerId = 3, Player = Players.First(p => p.Id == 3) }
        );
    }
}