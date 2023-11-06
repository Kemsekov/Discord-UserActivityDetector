
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using DiscordBotServer.App;
using Laraue.EfCoreTriggers.PostgreSql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TelegramBot.App.Helpers;

/// <summary>
/// Place that contains reference to global application running, so it could be accessed from everywhere
/// </summary>
public static class AppInstance
{
    public static IHost? App { get; set; }
    public static IServiceProvider? Services => App?.Services;
    public static void DebugConfiguration(HostBuilderContext context, IServiceCollection services)
    {
        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            options
                .UseNpgsql(context.Configuration.GetConnectionString("Db"))
                .UsePostgreSqlTriggers();
        });
        if(EF.IsDesignTime) return;
        services.AddSingleton<DiscordSocketConfig>(s=>new(){
            //here configure discord bot
            GatewayIntents=GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent | GatewayIntents.GuildPresences,
        });
        services.AddSingleton<DiscordSocketClient>();
        
        services.AddSingletonFromAssembly<ISlashCommandSource>(typeof(ISlashCommandSource).Assembly);
        services.AddSingletonFromAssembly<ISlashCommandHandler>(typeof(ISlashCommandHandler).Assembly);
        services.AddSingleton<IPresenceUpdateHandler,PresenceUpdateHandler>();
        services.AddHostedService<BotHostedService>();

    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
            config.AddJsonFile(
                "appsettings.json",
                optional: true,
                reloadOnChange: true)
            .AddJsonFile(
                "secrets.json",
                optional: true,
                reloadOnChange: true)
        );
}
