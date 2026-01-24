using Database.Models;

namespace BettySlotGame.Services.Interfaces;

public interface IGameSessionService
{
    public Task<int> CreateGameSession(decimal balance);
    public Task<GameSessionEntity> GetGameSession(int gameSessionId);
}