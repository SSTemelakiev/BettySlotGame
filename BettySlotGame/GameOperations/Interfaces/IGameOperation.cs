using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.GameOperations.Interfaces;

public interface IGameOperation
{
   public string ProcessOperation(IGameSessionService gameSessionSessionService, int gameSessionId, decimal betAmount);
}