using Discord;

namespace DiscordBotServer.App;

public interface ISlashCommandSource{
    SlashCommandBuilder CommandBuilder { get; }
}
