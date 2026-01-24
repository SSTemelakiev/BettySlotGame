using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame.GameOperations;

public class Withdraw(IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Withdraw;
    
    public string ProcessOperation(int gameSessionId, decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0) return AmountMustBePositiveMessage;
        
        var gameSession = gameSessionSessionService.GetGameSession(gameSessionId);
        
        var balance =  balanceService.GetBalance(gameSession);
        
        if (withdrawalAmount > balance) return InsufficientFundsForWithdrawalMessage;
        
        balance = balanceService.DecreaseBalance(gameSession, withdrawalAmount);
        
        return SuccessWithdrawalMessage(withdrawalAmount, balance);
    }
}