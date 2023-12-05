using System;
using Discord;
using Discord.WebSocket;
using DiscordBotServer.App.Models;
namespace DiscordBotServer.App;

public interface IPresenceUpdateHandler
{
    Task Handle(DiscordSocketClient client, SocketUser user, SocketPresence oldPresence, SocketPresence newPresence);
}

/// <summary>
/// This handler adds new row to database presence table, and prints new message to
/// guild's log chat about user presence change.
/// </summary>
public class PresenceUpdateHandler : IPresenceUpdateHandler
{
    private ApplicationDbContext _db;

    public PresenceUpdateHandler(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task Handle(DiscordSocketClient client, SocketUser user, SocketPresence oldPresence, SocketPresence newPresence)
    {
        await Task.Yield();
#pragma warning disable
        Task.Run(async () =>
        {
            var online = newPresence.Status == UserStatus.Online;
            var offline = newPresence.Status == UserStatus.Offline;
            if (!online && !offline) return;
            if (!_db.Users.Any(x => x.Id == user.Id))
                _db.Users.Add(new User() { Id = user.Id });
            var log = new UserPresenceLog() { Id = (ulong)Random.Shared.NextInt64(), DateTime = DateTime.UtcNow, Online = online, UserId = user.Id };
            _db.PresenceLogs.Add(log);
            _db.SaveChanges();

            foreach (var g in user.MutualGuilds)
            {
                var guild = _db.Guilds.FirstOrDefault(x => x.Id == g.Id);
                if (guild is null) continue;

                var logChannel = client.GetChannel(guild.ChannelLogId) as IMessageChannel;
                if (logChannel is null) continue;

                // replace with raw http
                await logChannel.SendMessageAsync($"@{user.GlobalName} теперь {newPresence.Status.ToString().ToLower()}");
            }
            var lastPresence = _db.PresenceLogs
            .OrderByDescending(x => x.DateTime)
            .Select(u => new { Username = client.GetUser(u.UserId), LastPresence = u.DateTime })
            .First();

            // replace with raw http
            // long unixTime = ((DateTimeOffset)lastPresence.LastPresence.ToUniversalTime()).ToUnixTimeSeconds();
            await client.SetGameAsync(string.Join("\n", new[] { lastPresence.Username.ToString(), lastPresence.LastPresence.ToUniversalTime().ToString() }));
        });
    }
}