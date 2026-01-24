using Database.Models;

namespace BettySlotGame.Services.Interfaces;

public interface IGameSessionService
{
    public int CreateGameSession(decimal balance);
    public GameSessionEntity GetGameSession(int gameId);
}