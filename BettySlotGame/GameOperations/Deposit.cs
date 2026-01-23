using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Deposit : IGameOperation
{
    public override string ToString() => "deposit";
    
    public string ProcessOperation(IGameSessionService gameSessionSessionService, int gameSessionId, decimal depositAmount)
    {
        if (depositAmount <= 0) return "Amount must be positive.";
        
        var balance = gameSessionSessionService.IncreaseBalance(gameSessionId, depositAmount);
        
        return $"Your deposit of ${depositAmount} was successful. Your current balance is: ${balance}";
    }
}