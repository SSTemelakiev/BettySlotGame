using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame.GameOperations;

public class Deposit(IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Deposit;
    
    public string ProcessOperation(int gameSessionId, decimal depositAmount)
    {
        if (depositAmount <= 0) return AmountMustBePositiveMessage;
        
        var gameSession = gameSessionSessionService.GetGameSession(gameSessionId);
        
        var balance = balanceService.IncreaseBalance(gameSession, depositAmount);
        
        return SuccessDepositMessage(depositAmount, balance);
    }
}