using BettySlotGame.Constants;
using BettySlotGame.Extensions;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame.GameOperations;

public class Bet(
    IRandomProvider randomProvider,
    GameStateService gameStateService,
    IGameSessionService gameSessionSessionService,
    IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Bet;

    public async Task<string> ProcessOperation(decimal betAmount)
    {
        var gameSession = await gameSessionSessionService.GetGameSession(gameStateService.CurrentSessionId);
        var balance = await balanceService.GetBalance(gameSession);

        if (balance < 1) return BalanceLessThanMinimumBetAmount;

        if (betAmount > balance) return InsufficientFundsMessage;

        if (betAmount < 1 || betAmount > 10) return InvalidBetAmountMessage;

        var roll = randomProvider.GetRandomNumberForWinChance();
        decimal multiplier;
        decimal winAmount;

        switch (roll)
        {
            case <= 50:
                balance = await balanceService.DecreaseBalance(gameSession, betAmount);
                return LoseMessage(balance);
            case <= 90:
                multiplier = randomProvider.GetRandomMultiplierForSmallWin();
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();

                await balanceService.DecreaseBalance(gameSession, betAmount);
                balance = await balanceService.IncreaseBalance(gameSession, winAmount);

                return WinMessage(winAmount, balance);
            default:
                multiplier = randomProvider.GetRandomMultiplierForBigWin();
                winAmount = (betAmount * multiplier).RoundToTwoDecimals();

                await balanceService.DecreaseBalance(gameSession, betAmount);
                balance = await balanceService.IncreaseBalance(gameSession, winAmount);

                return WinMessage(winAmount, balance);
        }
    }
}