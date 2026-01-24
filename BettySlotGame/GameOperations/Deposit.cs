using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Deposit(IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => "deposit";
    
    public string ProcessOperation(int gameSessionId, decimal depositAmount)
    {
        if (depositAmount <= 0) return "Amount must be positive.";
        
        var gameSession = gameSessionSessionService.GetGameSession(gameSessionId);
        
        var balance = balanceService.IncreaseBalance(gameSession, depositAmount);
        
        return $"Your deposit of ${depositAmount} was successful. Your current balance is: ${balance}";
    }
}