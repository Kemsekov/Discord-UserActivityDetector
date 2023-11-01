using DiscordBotServer.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DiscordBotServer.App.Configuration;

public class UserPresenceLogConfiguration : IEntityTypeConfiguration<UserPresenceLog>
{
    public void Configure(EntityTypeBuilder<UserPresenceLog> builder)
    {
        builder.HasOne(x=>x.User);
        builder.HasIndex(x=>x.UserId);
        builder.HasNoKey();
    }
}