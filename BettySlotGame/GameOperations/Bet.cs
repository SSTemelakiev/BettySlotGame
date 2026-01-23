using BettySlotGame.Extensions;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Bet : IGameOperation
{
    public override string ToString() => "bet";

    public string ProcessOperation(IGameSessionService gameSessionSessionService, int gameSessionId,
        decimal betAmount)
    {
        var balance = gameSessionSessionService.GetBalance(gameSessionId);

        if (balance < 1) return "Insufficient funds. Your balance is less than minimum bet betAmount of $1.";

        if (betAmount > balance) return "Insufficient funds. BetAmount betAmount must be less than your current balance.";

        if (betAmount < 1 || betAmount > 10) return "BetAmount betAmount must be between $1 and $10.";

        var roll = Random.Shared.Next(1, 101);
        decimal multiplier = 0;
        decimal winAmount = 0;

        switch (roll)
        {
            case <= 50:
                balance = gameSessionSessionService.DecreaseBalance(gameSessionId, betAmount);
                return $"No luck this time! Your current balance is  ${balance}";
            case <= 90:
                multiplier = (decimal)(Random.Shared.NextDouble() * (2.0 - 0.1) + 0.1);
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();
                
                balance = gameSessionSessionService.DecreaseBalance(gameSessionId, betAmount);
                balance = gameSessionSessionService.IncreaseBalance(gameSessionId, winAmount);
                
                return $"Congrats - you won ${winAmount}! Your current balance is  ${balance}";
            default:
                multiplier = (decimal)(Random.Shared.NextDouble() * (10.0 - 2.0) + 2.0);
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();
                
                balance = gameSessionSessionService.DecreaseBalance(gameSessionId, betAmount);
                balance = gameSessionSessionService.IncreaseBalance(gameSessionId, winAmount);
                
                return $"Congrats - you won ${winAmount}! Your current balance is  ${balance}";
        }
    }
}