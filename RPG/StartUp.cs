using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.ScreenServices;
using RPG.Core.Interfaces.ScreenServices.CharacterSelectServices;
using RPG.Core.Interfaces.ScreenServices.InGameServices;
using RPG.Core.Services;
using RPG.Core.Services.GameServices;
using RPG.Core.Services.GameServices.CharacterSelectServices;
using RPG.Core.Services.GameServices.InGameServices;
using RPG.Data;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        ConfigureConsoleOutput();

        var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<RPGDbContext>(options =>
            options.UseSqlServer(connectionString));

        AddGameScreenServices(services);

    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<RPGDbContext>();
    dbContext.Database.Migrate();

    var screenService = services.GetRequiredService<IScreenService>();
    screenService.ManageGameScreen();
}

void ConfigureConsoleOutput()
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;
}

void AddGameScreenServices(IServiceCollection services)
{
    services.AddScoped<IScreenService, ScreenService>();
    services.AddScoped<IMainMenuService, MainMenuService>();
    services.AddScoped<ICharacterSelectService, CharacterSelectService>();
    services.AddScoped<IInGameService, InGameService>();
    services.AddScoped<IUserInputService, UserInputService>();
    services.AddScoped<ICreateCharacterService, CreateCharacterService>();
    services.AddScoped<ICharacterRaceService, CharacterRaceService>();
    services.AddScoped<ICreateMonsterService, CreateMonsterService>();
    services.AddScoped<ICreateGameSessionService, CreateGameSessionService>();
    services.AddScoped<ICharacterTurnService, CharacterTurnService>();
    services.AddScoped<IMonsterTurnService, MonsterTurnService>();
    services.AddScoped<IGameFieldService, GameFieldService>();
}