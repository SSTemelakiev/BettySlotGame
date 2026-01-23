using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Withdraw : IGameOperation
{
    public override string ToString() => "withdraw";
    
    public decimal ProcessOperation(IGameService gameService, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void DisplayBalance(decimal balance)
    {
        throw new NotImplementedException();
    }
}