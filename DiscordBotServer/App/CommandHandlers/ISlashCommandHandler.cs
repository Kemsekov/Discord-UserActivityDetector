using Discord.WebSocket;
namespace DiscordBotServer.App;

public interface ISlashCommandHandler{
    public string CommandName{get;}
    public Task Handle(SocketSlashCommand command, DiscordSocketClient client);
}
