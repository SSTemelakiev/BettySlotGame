using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Withdraw(IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => "withdraw";
    
    public string ProcessOperation(int gameSessionId, decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0) return "Amount must be positive.";
        
        var gameSession = gameSessionSessionService.GetGameSession(gameSessionId);
        
        var balance =  balanceService.GetBalance(gameSession);
        
        if (withdrawalAmount > balance) return "Insufficient funds for withdrawal.";
        
        balance = balanceService.DecreaseBalance(gameSession, withdrawalAmount);
        
        return $"Your withdrawal of ${withdrawalAmount} was successful. Your current balance is: ${balance}";
    }
}