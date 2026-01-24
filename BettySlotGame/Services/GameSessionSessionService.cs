using BettySlotGame.Constants;
using BettySlotGame.Extensions;
using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static BettySlotGame.Constants.LoggerMessages;

namespace BettySlotGame.Services;

public class GameSessionSessionService(BettySlotGameDbContext context, ILogger<GameSessionSessionService> logger) : IGameSessionService
{
    public async Task<int> CreateGameSession(decimal balance)
    {
        try
        {
            logger.LogInformation(CreateGameSessionMessage(balance));
        
            var gameSession = new GameSessionEntity { Balance = balance.RoundToTwoDecimals() };
            context.GameSessions.Add(gameSession);
            await context.SaveChangesAsync();
        
            logger.LogInformation(SuccessfullyCreatedGameSessionMessage(gameSession.Id));
            return gameSession.Id;
        }
        catch (Exception e)
        {
            logger.LogError(e, FailedToCreateGameSessionMessage(e.Message));
            throw;
        }
    }

    public async Task<GameSessionEntity> GetGameSession(int gameSessionId)
    {
        logger.LogDebug(GetGameSessionMessage(gameSessionId));
        
        var gameSession = await context.GameSessions.FirstOrDefaultAsync(g => g.Id == gameSessionId);
        
        if (gameSession == null)
        {
            logger.LogWarning(GameSessionNotFoundMessage(gameSessionId));
            throw new KeyNotFoundException(GameSessionNotFoundMessage(gameSessionId));
        }
        
        return gameSession;
    }
}