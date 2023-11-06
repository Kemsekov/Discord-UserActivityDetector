using Discord;
using Discord.WebSocket;
namespace DiscordBotServer.App;
/// <summary>
/// This command allows to select chat that will be used to log user's presence changes
/// </summary>
public class SelectLogChatCommandHandler : ISlashCommandHandler
{
    private ApplicationDbContext _db;
    private SelectLogChatCommand _command;

    public SelectLogChatCommandHandler(ApplicationDbContext db, SelectLogChatCommand command){
        _db = db;
        _command = command;
    }
    public string CommandName=>_command.CommandBuilder.Name;
    public async Task Handle(SocketSlashCommand command, DiscordSocketClient client)
    {
        var channel = command.Data.Options.FirstOrDefault()?.Value as IChannel;
        var guildId = command.GuildId;
        if(channel is null){
            await command.RespondAsync($"Укажите чат первым параметром",ephemeral:true);
            return;
        }
        if(guildId is null) return;

        var g = _db.Guilds.FirstOrDefault(x=>x.Id==guildId);
        if(g is not null)
            _db.Remove(g);
        
        if(g is null){
            g = new Models.Guild(){Id=guildId ?? 0};
            _db.Add(g);    
        }
        g.ChannelLogId=channel.Id;
        

        _db.Update(g);
        _db.SaveChanges();
        await command.RespondAsync($"Выбран чат {channel.Name}",ephemeral:true);

        // var channelId = command.ChannelId ?? throw new Exception("I NEED NOT NULL CHANNEL ID BRO");
        // var g = _db.Guilds.FirstOrDefault(x=>x.Id==command.GuildId) ?? new Models.Guild(){Id=guildId,ChannelLogId=channelId};
        // _db.Remove(g);
        // _db.Add(g);
        // _db.SaveChanges();
    }
}
