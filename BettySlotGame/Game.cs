using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Helpers;
using BettySlotGame.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BettySlotGame;

public static class Game
{
    public static void Start(IHost host)
    {
        var exit = false;
        var gameSessionId = 0;
        
        while (!exit)
        {
            Console.WriteLine("Please submit action:");
            var command = Console.ReadLine();

            using var scope = host.Services.CreateScope();
            var gameService = scope.ServiceProvider.GetRequiredService<IGameSessionService>();
            var gameOperations = scope.ServiceProvider.GetServices<IGameOperation>();
                
            try
            {
                var action = InputProcessor.ReadAction(command);

                if (action.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Thank you for playing! Hope to see you again soon.");
                    exit = true;
                    continue;
                }

                var strategy = gameOperations.FirstOrDefault(o => 
                    o.ToString()!.Equals(action, StringComparison.OrdinalIgnoreCase));

                if (strategy != null)
                {
                    if (gameSessionId == 0) gameSessionId = gameService.CreateGameSession(0);
                    
                    var displayMessage = strategy.ProcessOperation(gameService, gameSessionId, InputProcessor.ReadAmount(command));
                    Console.WriteLine(displayMessage);
                }
                else
                {
                    Console.WriteLine($"Unknown command: {command}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
        }
    }
}