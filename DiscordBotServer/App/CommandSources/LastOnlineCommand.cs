using System;
using Discord;
namespace DiscordBotServer.App;
public class LastOnlineCommand : ISlashCommandSource
{
    public LastOnlineCommand()
    {
        var lastOnline = new SlashCommandBuilder();
        lastOnline.WithName("last_online");
        lastOnline.WithDescription("получить последние n посещений юзера");

        var usernameOption = new SlashCommandOptionBuilder();
        usernameOption.WithName("user");
        usernameOption.WithType(ApplicationCommandOptionType.User);
        usernameOption.WithDescription("имя юзера по которому нужно получить информацию");
        lastOnline.AddOption(usernameOption);

        var NOption = new SlashCommandOptionBuilder();
        NOption.WithName("n");
        NOption.WithType(ApplicationCommandOptionType.Integer);
        NOption.WithDescription("количество посещений которые нужно отобразить");
        lastOnline.AddOption(NOption);
        CommandBuilder = lastOnline;
    }

    public SlashCommandBuilder CommandBuilder{get;}
}
