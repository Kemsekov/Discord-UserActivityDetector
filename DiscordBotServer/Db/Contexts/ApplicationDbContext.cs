using DiscordBotServer.App.Configuration;
using DiscordBotServer.App.Models;
using Microsoft.EntityFrameworkCore;
namespace DiscordBotServer.App;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Guild> Guilds { get; set; }
    public DbSet<UserPresenceLog> PresenceLogs { get; set; }
}