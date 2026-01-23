using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Withdraw : IGameOperation
{
    public override string ToString() => "withdraw";
    
    public string ProcessOperation(IGameSessionService gameSessionSessionService, int gameSessionId, decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0) return "Amount must be positive.";
        
        var balance =  gameSessionSessionService.GetBalance(gameSessionId);
        
        if (withdrawalAmount > balance) return "Insufficient funds for withdrawal.";
        
        balance = gameSessionSessionService.DecreaseBalance(gameSessionId, withdrawalAmount);
        
        return $"Your withdrawal of ${withdrawalAmount} was successful. Your current balance is: ${balance}";
    }
}