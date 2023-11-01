using DiscordBotServer.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DiscordBotServer.App.Configuration;

public class GuildConfiguration : IEntityTypeConfiguration<Guild>
{
    public void Configure(EntityTypeBuilder<Guild> builder)
    {
        builder.HasKey(g=>g.Id);
        builder.Property(g=>g.ChannelLogId).HasDefaultValue(0).IsRequired();
    }
}
