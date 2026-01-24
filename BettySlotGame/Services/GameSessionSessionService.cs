using BettySlotGame.Constants;
using BettySlotGame.Extensions;
using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;
using Microsoft.Extensions.Logging;
using static BettySlotGame.Constants.LoggerMessages;

namespace BettySlotGame.Services;

public class GameSessionSessionService(BettySlotGameDbContext context, ILogger<GameSessionSessionService> logger) : IGameSessionService
{
    public int CreateGameSession(decimal balance)
    {
        try
        {
            logger.LogInformation(CreateGameSessionMessage(balance));
        
            var gameSession = new GameSessionEntity { Balance = balance.RoundToTwoDecimals() };
            context.GameSessions.Add(gameSession);
            context.SaveChanges();
        
            logger.LogInformation(SuccessfullyCreatedGameSessionMessage(gameSession.Id));
            return gameSession.Id;
        }
        catch (Exception e)
        {
            logger.LogError(e, FailedToCreateGameSessionMessage(e.Message));
            throw;
        }
    }

    public GameSessionEntity GetGameSession(int gameSessionId)
    {
        logger.LogDebug(GetGameSessionMessage(gameSessionId));
        
        var gameSession =  context.GameSessions.FirstOrDefault(g => g.Id == gameSessionId);
        
        if (gameSession == null)
        {
            logger.LogWarning(GameSessionNotFoundMessage(gameSessionId));
            throw new KeyNotFoundException(GameSessionNotFoundMessage(gameSessionId));
        }
        
        return gameSession;
    }
}