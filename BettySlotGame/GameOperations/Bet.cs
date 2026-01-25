using BettySlotGame.Constants;
using BettySlotGame.Extensions;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using Database.Models;
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

        return randomProvider.GetRandomNumberForWinChance() switch
        {
            <= 50 => await PlaceLost(gameSession, betAmount),
            <= 90 => await PlaceWin(gameSession, betAmount, randomProvider.GetRandomMultiplierForSmallWin()),
            _ => await PlaceWin(gameSession, betAmount, randomProvider.GetRandomMultiplierForBigWin())
        };
    }

    private async Task<string> PlaceLost(GameSessionEntity gameSession, decimal betAmount)
    {
        var balance = await balanceService.DecreaseBalance(gameSession, betAmount);
        return LoseMessage(balance);
    }

    private async Task<string> PlaceWin(GameSessionEntity gameSession, decimal betAmount, decimal multiplier)
    {
        var winAmount = (betAmount * multiplier).RoundToTwoDecimals();

        await balanceService.DecreaseBalance(gameSession, betAmount);
        var balance = await balanceService.IncreaseBalance(gameSession, winAmount);

        return WinMessage(winAmount, balance);
    }
}