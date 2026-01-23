using BettySlotGame.GameOperations.Interfaces;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations;

public class Bet : IGameOperation
{
    public override string ToString() => "bet";

    public decimal ProcessOperation(IGameService gameService, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void DisplayBalance(decimal balance)
    {
        throw new NotImplementedException();
    }
}