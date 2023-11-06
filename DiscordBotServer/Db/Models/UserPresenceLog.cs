namespace DiscordBotServer.App.Models;

public class UserPresenceLog{
    public ulong Id{get;set;}
    public ulong UserId{get;set;}
    public User? User{get;set;}
    public bool Online{get;set;}
    public required DateTime DateTime{get;set;}
}

