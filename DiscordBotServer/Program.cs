using static AppInstance;
//https://discord.com/api/oauth2/authorize?client_id=1168507606822834206&permissions=8589936640&scope=bot


var builder = CreateHostBuilder(args);
builder.ConfigureServices(DebugConfiguration);
App = builder.Build();
App.Run();