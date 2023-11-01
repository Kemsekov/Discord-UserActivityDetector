using System;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

// create - send message on presence change
// read - read users list from discord server
// update - update bot subtitle - it will contains last 3 logged peoples
// delete - clear log chat

namespace DiscordBotServer.App;
public class BotHostedService : IHostedService
{
    private Dictionary<string, ISlashCommandHandler> _slashCommandHandlers;
    private IEnumerable<ISlashCommandSource> _commandsSource;
    private ApplicationDbContext _db;
    private DiscordSocketClient _discordClient;
    public BotHostedService(
        DiscordSocketClient discordClient, 
        IConfiguration conf, 
        ILogger<IDiscordClient> logger, 
        ApplicationDbContext db,
        IEnumerable<ISlashCommandSource> commandsSource,
        IEnumerable<ISlashCommandHandler> slashCommandHandlers)
    {
        _slashCommandHandlers = slashCommandHandlers.ToDictionary(x=>x.CommandName);
        _commandsSource = commandsSource;
        _db = db;
        _discordClient = discordClient;
        _discordClient.Log += e=>{
            logger.Log(LogLevel.Information,e.ToString());
            return Task.CompletedTask;
        };
        _discordClient.PresenceUpdated+= async (u,pOld,pNew)=>{
            if(u is IGuildUser ug){
                System.Console.WriteLine(ug.Guild.Name);
                var chats = await ug.Guild.GetTextChannelsAsync();
            }
            System.Console.WriteLine("presence updated");
            System.Console.WriteLine(u.Username);
            System.Console.WriteLine(u.Status);
        };
        
        _discordClient.SlashCommandExecuted += 
            command=>_slashCommandHandlers[command.CommandName].Handle(command,_discordClient);
        
        _discordClient.LoginAsync(TokenType.Bot,conf["Token"]).Wait();
        _discordClient.DownloadUsersAsync(_discordClient.Guilds);
        discordClient.Ready += RegisterCommands;
    }
    public Task RegisterCommands(){
        foreach(var g in _discordClient.Guilds)
        foreach(var c in _commandsSource)
            g.CreateApplicationCommandAsync(c.CommandBuilder.Build());
        return Task.CompletedTask;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _discordClient.StartAsync();
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _discordClient.StopAsync();
    }
}
