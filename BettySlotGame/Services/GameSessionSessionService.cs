using BettySlotGame.Extensions;
using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;

namespace BettySlotGame.Services;

public class GameSessionSessionService(BettySlotGameDbContext context) : IGameSessionService
{
    public int CreateGameSession(decimal balance)
    {
        var gameSession = new GameSessionEntity { Balance = balance.RoundToTwoDecimals() };
        context.GameSessions.Add(gameSession);
        context.SaveChanges();
        return gameSession.Id;
    }

    public GameSessionEntity GetGameSession(int gameId)
    {
        var gameSession =  context.GameSessions.FirstOrDefault(g => g.Id == gameId);
        
        return gameSession ?? throw new KeyNotFoundException($"Game with {gameId} was not found.");
    }
}