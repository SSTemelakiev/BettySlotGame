using BettySlotGame.Constants;
using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using static BettySlotGame.Constants.DisplayMessages;

namespace BettySlotGame.GameOperations;

public class Withdraw(GameStateService gameStateService, IGameSessionService gameSessionSessionService, IBalanceService balanceService) : IGameOperation
{
    public override string ToString() => CommandNames.Withdraw;
    
    public string ProcessOperation(decimal withdrawalAmount)
    {
        if (withdrawalAmount <= 0) return AmountMustBePositiveMessage;
        
        var gameSession = gameSessionSessionService.GetGameSession(gameStateService.CurrentSessionId);
        
        var balance =  balanceService.GetBalance(gameSession);
        
        if (withdrawalAmount > balance) return InsufficientFundsForWithdrawalMessage;
        
        balance = balanceService.DecreaseBalance(gameSession, withdrawalAmount);
        
        return SuccessWithdrawalMessage(withdrawalAmount, balance);
    }
}