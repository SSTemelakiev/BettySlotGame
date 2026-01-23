using System.Text;
using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations.Interfaces;

public interface IGameOperation
{
   public decimal ProcessOperation(IGameService gameService, decimal amount);

   public void DisplayBalance(decimal balance);
}