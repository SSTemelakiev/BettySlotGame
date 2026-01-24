using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations.Interfaces;

public interface IGameOperation
{
   public Task<string> ProcessOperation(decimal betAmount);
}