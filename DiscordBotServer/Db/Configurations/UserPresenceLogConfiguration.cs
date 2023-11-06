using DiscordBotServer.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DiscordBotServer.App.Configuration;

public class UserPresenceLogConfiguration : IEntityTypeConfiguration<UserPresenceLog>
{
    public void Configure(EntityTypeBuilder<UserPresenceLog> builder)
    {
        builder.HasOne(x=>x.User);
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
    }
}