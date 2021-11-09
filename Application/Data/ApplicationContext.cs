using Microsoft.EntityFrameworkCore;

namespace Application.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GamePlayer> GamePlayers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=store;Username=postgres;Password=postgres");
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GameGenre>().HasKey(i => new { i.GameId, i.GenreId });
            builder.Entity<GamePlatform>().HasKey(i => new { i.GameId, i.PlatformId });
            builder.Entity<GamePlayer>().HasKey(i => new { i.GameId, i.PlayerId });
        }
    }
}