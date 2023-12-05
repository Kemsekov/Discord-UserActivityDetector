using Discord;
namespace DiscordBotServer.App;

public class AllOnlineCommand : ISlashCommandSource
{
    public AllOnlineCommand()
    {
        var lastOnline = new SlashCommandBuilder();
        lastOnline.WithName("all_online");
        lastOnline.WithDescription("получить последние n посещений сервера");

        var NOption = new SlashCommandOptionBuilder();
        NOption.WithName("n");
        NOption.WithType(ApplicationCommandOptionType.Integer);
        NOption.WithDescription("количество посещений которые нужно отобразить");
        lastOnline.AddOption(NOption);
        CommandBuilder = lastOnline;
    }

    public SlashCommandBuilder CommandBuilder{get;}
}