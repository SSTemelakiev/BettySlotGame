using BettySlotGame.Constants;
using BettySlotGame.Extensions;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Bet(IRandomProvider randomProvider, IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Bet;

    public string ProcessOperation(int gameSessionId, decimal betAmount)
    {
        var gameSession =  gameSessionSessionService.GetGameSession(gameSessionId);
        var balance = balanceService.GetBalance(gameSession);

        if (balance < 1) return DisplayMessages.BalanceLessThanMinimumBetAmount;

        if (betAmount > balance) return DisplayMessages.InsufficientFundsMessage;

        if (betAmount < 1 || betAmount > 10) return DisplayMessages.InvalidBetAmountMessage;

        var roll = randomProvider.GetRandomNumberForWinChance();
        decimal multiplier = 0;
        decimal winAmount = 0;

        switch (roll)
        {
            case <= 50:
                balance = balanceService.DecreaseBalance(gameSession, betAmount);
                return DisplayMessages.LoseMessage(balance);
            case <= 90:
                multiplier = randomProvider.GetRandomMultiplierForSmallWin();
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();
                
                balanceService.DecreaseBalance(gameSession, betAmount);
                balance = balanceService.IncreaseBalance(gameSession, winAmount);
                
                return DisplayMessages.WinMessage(winAmount, balance);
            default:
                multiplier = randomProvider.GetRandomMultiplierForBigWin();
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();
                
                balanceService.DecreaseBalance(gameSession, betAmount);
                balance = balanceService.IncreaseBalance(gameSession, winAmount);
                
                return DisplayMessages.WinMessage(winAmount, balance);
        }
    }
}