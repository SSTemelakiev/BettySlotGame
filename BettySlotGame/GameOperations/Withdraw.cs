using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame.GameOperations;

public class Withdraw(
    GameStateService gameStateService,
    IGameSessionService gameSessionSessionService,
    IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Withdraw;

    public async Task<string> ProcessOperation(decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0) return AmountMustBePositiveMessage;

        var gameSession = await gameSessionSessionService.GetGameSession(gameStateService.CurrentSessionId);

        var balance = await balanceService.GetBalance(gameSession);

        if (withdrawalAmount > balance) return InsufficientFundsForWithdrawalMessage;

        balance = await balanceService.DecreaseBalance(gameSession, withdrawalAmount);

        return SuccessWithdrawalMessage(withdrawalAmount, balance);
    }
}