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
        Database.EnsureCreated();
        Database.Migrate();
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
    }
}