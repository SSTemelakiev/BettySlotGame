using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations.Interfaces;

public interface IGameOperation
{
   public string ProcessOperation(int gameSessionId, decimal betAmount);
}