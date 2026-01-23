using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Deposit : IGameOperation
{
    public override string ToString() => "deposit";
    
    public decimal ProcessOperation(IGameService gameService, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void DisplayBalance(decimal balance)
    {
        throw new NotImplementedException();
    }
}