using System.Linq;
using System.Text;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
namespace DiscordBotServer.App;

/// <summary>
/// /last_online userName n
/// prints message that only sender of command can see, last n times user's presence
/// have changed from offline to online
/// </summary>
public class LastOnlineCommandHandler : ISlashCommandHandler
{
    private ApplicationDbContext _db;
    private LastOnlineCommand _command;

    public LastOnlineCommandHandler(ApplicationDbContext db, LastOnlineCommand command)
    {
        _db = db;
        _command = command;
    }
    public string CommandName => _command.CommandBuilder.Name;
    public async Task Handle(SocketSlashCommand command, DiscordSocketClient client)
    {
        await Task.Yield();
#pragma warning disable
        Task.Run(async () =>
        {
            //insert useless raw https that gets all server's users, and uses one with Id eq to command.Data.Options[0]
            var user = (SocketUser)command.Data.Options.ElementAt(0).Value;
            Int64 n = 1;
            if (command.Data.Options.Count() > 1)
                n = (Int64)command.Data.Options.ElementAt(1).Value;
            var logs = _db.PresenceLogs.OrderByDescending(x => x.DateTime).Where(x => x.UserId == user.Id).Take((int)n).ToArray();
            var result = new StringBuilder();
            if (logs.Length != 0)
                result.Append($"Последний онлайн {user.GlobalName}\n");
            else
                result.Append($"Активность отсутствует для {user.GlobalName}\n");
            foreach (var log in logs)
            {
                result.Append($"{log.DateTime} {(log.Online ? "Online" : "Offline")}\n");
            }
            await command.RespondAsync(result.ToString(), ephemeral: true);
        });
    }
}
