using Discord;
namespace DiscordBotServer.App;

public class SelectLogChatCommand : ISlashCommandSource
{
    public SelectLogChatCommand()
    {
        //selects chat that can be used to log users presence info
        var chatOption = new SlashCommandOptionBuilder();
        chatOption.WithName("chat");
        chatOption.WithType(ApplicationCommandOptionType.Channel);
        chatOption.WithDescription("куда скидывать сообщение о появления юзера в сети");

        var selectLogChatCommand = new SlashCommandBuilder();
        selectLogChatCommand.WithName("select_chat");
        selectLogChatCommand.WithDescription("куда скидывать логи появления юзеров сервера в онлайне");
        selectLogChatCommand.AddOption(chatOption);
        CommandBuilder = selectLogChatCommand;
    }
    public SlashCommandBuilder CommandBuilder { get; }
}
