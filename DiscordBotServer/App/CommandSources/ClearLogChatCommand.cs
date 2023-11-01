using Discord;
namespace DiscordBotServer.App;

public class ClearLogChatCommand : ISlashCommandSource
{
    public ClearLogChatCommand()
    {
        //clears chat(if selected) that is used to log users
        var clearLogChat = new SlashCommandBuilder();
        clearLogChat.WithName("clear_chat");
        clearLogChat.WithDescription("очистить чат куда скидываются логи появления юзеров сервера в онлайне");
        CommandBuilder = clearLogChat;
    }

    public SlashCommandBuilder CommandBuilder { get; }
}
