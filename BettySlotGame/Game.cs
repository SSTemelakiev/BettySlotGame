using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Helpers;
using BettySlotGame.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame;

public static class Game
{
    public static void Start(IHost host)
    {
        var exit = false;
        var gameSessionId = 0;
        
        while (!exit)
        {
            Console.WriteLine(SubmitActionMessage);
            var command = Console.ReadLine();

            using var scope = host.Services.CreateScope();
            var gameService = scope.ServiceProvider.GetRequiredService<IGameSessionService>();
            var gameOperations = scope.ServiceProvider.GetServices<IGameOperation>();
                
            try
            {
                var action = InputProcessor.ReadAction(command);

                if (action.Equals(CommandNames.Exit, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(EndGameMessage);
                    exit = true;
                    continue;
                }

                var strategy = gameOperations.FirstOrDefault(o => 
                    o.ToString()!.Equals(action, StringComparison.OrdinalIgnoreCase));

                if (strategy != null)
                {
                    if (gameSessionId == 0) gameSessionId = gameService.CreateGameSession(0);
                    
                    var displayMessage = strategy.ProcessOperation(gameSessionId, InputProcessor.ReadAmount(command));
                    Console.WriteLine(displayMessage);
                }
                else
                {
                    Console.WriteLine(UnknownCommandMessage(command));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(ExceptionMessage(e.Message));
                throw;
            }
        }
    }
}