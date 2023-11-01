namespace DiscordBotServer.App.Models;

public class User
{
    public ulong Id { get; set; }
    public override string ToString()
    {
        return $"User {Id}";
    }
}

