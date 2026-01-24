using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations.Interfaces;

public interface IGameOperation
{
   public string ProcessOperation(decimal betAmount);
}