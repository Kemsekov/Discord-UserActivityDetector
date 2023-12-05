using System.Text;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
namespace DiscordBotServer.App;

/// <summary>
/// /all_online n
/// </summary>
public class AllOnlineCommandHandler : ISlashCommandHandler
{
    private ApplicationDbContext _db;
    private AllOnlineCommand _command;

    public AllOnlineCommandHandler(ApplicationDbContext db, AllOnlineCommand command)
    {
        _db = db;
        _command = command;
    }
    public string CommandName => _command.CommandBuilder.Name;
    public async Task Handle(SocketSlashCommand command, DiscordSocketClient client)
    {
        await Task.Yield();
#pragma warning disable
            Int64 n = 1;
            if (command.Data.Options.Count() > 0)
                n = (Int64)command.Data.Options.First().Value;
            if (command.GuildId is null) return;
            var guild = client.GetGuild(command.GuildId ?? 0);

            var users = guild.Users.ToArray();
            var usersId =users.Select(u => u.Id).ToArray();
            var logs = _db.PresenceLogs.Where(l=>usersId.Contains(l.UserId)).OrderByDescending(x => x.DateTime).Take((int)n).ToArray();
            var result = new StringBuilder();
            if (logs.Length != 0)
                result.Append($"Последний онлайн сервера\n");
            else
                result.Append($"Активность отсутствует для сервера\n");
            var userLogs = users.Join(logs,u=>u.Id,l=>l.UserId,(u,l)=>(u,l));
            foreach (var userLog in userLogs)
            {
                var log = userLog.l;
                var user = userLog.u;
                long unixTime = ((DateTimeOffset)log.DateTime.ToUniversalTime()).ToUnixTimeSeconds();
                result.Append($"{user.Username} <t:{unixTime}:f> {(log.Online ? "Online" : "Offline")}\n");
            }
            await command.RespondAsync(result.ToString(), ephemeral: true);
    }
}