using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BettySlotGame.extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BettySlotGameDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        var operationsTypes = typeof(IGameOperation).Assembly.GetTypes()
            .Where(t => typeof(IGameOperation).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var operation in operationsTypes)
        {
            services.AddScoped(typeof(IGameOperation), operation);
        }
        
        services.AddSingleton<GameStateService>();

        services.AddScoped<IGameSessionService, GameSessionService>();
        services.AddScoped<IBalanceService, BalanceService>();
        services.AddScoped<IRandomProvider, RandomProvider>();
    }
}