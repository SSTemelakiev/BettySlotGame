using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Withdraw(IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Withdraw;
    
    public string ProcessOperation(int gameSessionId, decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0) return DisplayMessages.AmountMustBePositiveMessage;
        
        var gameSession = gameSessionSessionService.GetGameSession(gameSessionId);
        
        var balance =  balanceService.GetBalance(gameSession);
        
        if (withdrawalAmount > balance) return DisplayMessages.InsufficientFundsForWithdrawalMessage;
        
        balance = balanceService.DecreaseBalance(gameSession, withdrawalAmount);
        
        return DisplayMessages.SuccessWithdrawalMessage(withdrawalAmount, balance);
    }
}