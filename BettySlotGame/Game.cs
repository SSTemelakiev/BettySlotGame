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
        var gameService = host.Services.GetRequiredService<IGameService>();
        var gameOperations = host.Services.GetServices<IGameOperation>();
        
       // var newId = await gameService.CreateGame(100.50m);

        var exit = false;
        
        while (!exit)
        {
            Console.WriteLine("Please submit action:");
            var command = Console.ReadLine();

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
                    var balance = strategy.ProcessOperation(gameService, InputProcessor.ReadAmount(action));
                    strategy.DisplayBalance(balance);
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