using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame.GameOperations;

public class Deposit(
    GameStateService gameStateService,
    IGameSessionService gameSessionSessionService,
    IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Deposit;

    public async Task<string> ProcessOperation(decimal depositAmount)
    {
        if (depositAmount <= 0) return AmountMustBePositiveMessage;

        var gameSession = await gameSessionSessionService.GetGameSession(gameStateService.CurrentSessionId);

        var balance = await balanceService.IncreaseBalance(gameSession, depositAmount);

        return SuccessDepositMessage(depositAmount, balance);
    }
}