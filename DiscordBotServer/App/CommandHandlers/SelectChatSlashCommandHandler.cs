using Discord.WebSocket;
namespace DiscordBotServer.App;
public class SelectChatCommandHandler : ISlashCommandHandler
{
    private ApplicationDbContext _db;

    public SelectChatCommandHandler(ApplicationDbContext db){
        _db = db;
    }
    public string CommandName=>"select_chat";
    public async Task Handle(SocketSlashCommand command, DiscordSocketClient client)
    {
        // command.Data
        await command.RespondAsync($"Чат {command.Channel.Name} выбран. {command.CommandName}",ephemeral:true);
        // var guildId = command.GuildId ?? throw new Exception("I NEED NOT NULL GUILD ID BRO");
        // var channelId = command.ChannelId ?? throw new Exception("I NEED NOT NULL CHANNEL ID BRO");
        // var g = _db.Guilds.FirstOrDefault(x=>x.Id==command.GuildId) ?? new Models.Guild(){Id=guildId,ChannelLogId=channelId};
        // _db.Remove(g);
        // _db.Add(g);
        // _db.SaveChanges();
    }
}

public class LastOnlineCommandHandler : ISlashCommandHandler
{
    private ApplicationDbContext _db;

    public LastOnlineCommandHandler(ApplicationDbContext db){
        _db = db;
    }
    public string CommandName=>"last_online";
    public async Task Handle(SocketSlashCommand command, DiscordSocketClient client)
    {
        await command.RespondAsync($"Последний онлайн. {command.CommandName}",ephemeral:true);
        // var guildId = command.GuildId ?? throw new Exception("I NEED NOT NULL GUILD ID BRO");
        // var channelId = command.ChannelId ?? throw new Exception("I NEED NOT NULL CHANNEL ID BRO");
        // var g = _db.Guilds.FirstOrDefault(x=>x.Id==command.GuildId) ?? new Models.Guild(){Id=guildId,ChannelLogId=channelId};
        // _db.Remove(g);
        // _db.Add(g);
        // _db.SaveChanges();
    }
}

public class ClearLogChatCommandHandler : ISlashCommandHandler
{
    private ApplicationDbContext _db;

    public ClearLogChatCommandHandler(ApplicationDbContext db){
        _db = db;
    }
    public string CommandName=>"clear_chat";
    public async Task Handle(SocketSlashCommand command, DiscordSocketClient client)
    {
        await command.RespondAsync($"Очистка чата. {command.CommandName}",ephemeral:true);
        // var guildId = command.GuildId ?? throw new Exception("I NEED NOT NULL GUILD ID BRO");
        // var channelId = command.ChannelId ?? throw new Exception("I NEED NOT NULL CHANNEL ID BRO");
        // var g = _db.Guilds.FirstOrDefault(x=>x.Id==command.GuildId) ?? new Models.Guild(){Id=guildId,ChannelLogId=channelId};
        // _db.Remove(g);
        // _db.Add(g);
        // _db.SaveChanges();
    }
}