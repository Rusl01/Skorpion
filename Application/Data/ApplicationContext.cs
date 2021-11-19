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
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GameGenre>().HasKey(i => new {i.GameId, i.GenreId});
        builder.Entity<GamePlatform>().HasKey(i => new {i.GameId, i.PlatformId});
        builder.Entity<GamePlayer>().HasKey(i => new {i.GameId, i.PlayerId});
    }
}