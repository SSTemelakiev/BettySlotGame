using BettySlotGame.Extensions;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Bet(IRandomProvider randomProvider, IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => "bet";

    public string ProcessOperation(int gameSessionId, decimal betAmount)
    {
        var gameSession =  gameSessionSessionService.GetGameSession(gameSessionId);
        var balance = balanceService.GetBalance(gameSession);

        if (balance < 1) return "Insufficient funds. Your balance is less than minimum bet betAmount of $1.";

        if (betAmount > balance) return "Insufficient funds. BetAmount betAmount must be less than your current balance.";

        if (betAmount < 1 || betAmount > 10) return "BetAmount betAmount must be between $1 and $10.";

        var roll = randomProvider.GetRandomNumberForWinChance();
        decimal multiplier = 0;
        decimal winAmount = 0;

        switch (roll)
        {
            case <= 50:
                balance = balanceService.DecreaseBalance(gameSession, betAmount);
                return $"No luck this time! Your current balance is  ${balance}";
            case <= 90:
                multiplier = randomProvider.GetRandomMultiplierForSmallWin();
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();
                
                balanceService.DecreaseBalance(gameSession, betAmount);
                balance = balanceService.IncreaseBalance(gameSession, winAmount);
                
                return $"Congrats - you won ${winAmount}! Your current balance is  ${balance}";
            default:
                multiplier = randomProvider.GetRandomMultiplierForBigWin();
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();
                
                balanceService.DecreaseBalance(gameSession, betAmount);
                balance = balanceService.IncreaseBalance(gameSession, winAmount);
                
                return $"Congrats - you won ${winAmount}! Your current balance is  ${balance}";
        }
    }
}