using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Deposit(IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Deposit;
    
    public string ProcessOperation(int gameSessionId, decimal depositAmount)
    {
        if (depositAmount <= 0) return DisplayMessages.AmountMustBePositiveMessage;
        
        var gameSession = gameSessionSessionService.GetGameSession(gameSessionId);
        
        var balance = balanceService.IncreaseBalance(gameSession, depositAmount);
        
        return DisplayMessages.SuccessDepositMessage(depositAmount, balance);
    }
}