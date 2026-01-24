using BettySlotGame.Extensions;
using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;
using Microsoft.Extensions.Logging;
using static BettySlotGame.Constants.LoggerMessages;

namespace BettySlotGame.Services;

public class BalanceService(BettySlotGameDbContext context, ILogger<BalanceService> logger) : IBalanceService
{
    public async Task<decimal> GetBalance(GameSessionEntity gameSession)
    {
        var balance = gameSession.Balance.RoundToTwoDecimals();
        logger.LogDebug(GetBalanceMessage(gameSession.Id, balance));
        return await Task.FromResult(balance);
    } 

    public async Task<decimal> IncreaseBalance(GameSessionEntity gameSession, decimal amount)
    {
        var oldBalance = gameSession.Balance;
        gameSession.Balance += amount.RoundToTwoDecimals();
        
        await context.SaveChangesAsync();
        
        logger.LogInformation(IncreaseBalanceMessage(gameSession.Id, amount, gameSession.Balance, oldBalance));
        
        return gameSession.Balance.RoundToTwoDecimals();
    }
    
    public async Task<decimal> DecreaseBalance(GameSessionEntity gameSession, decimal amount)
    {
        var oldBalance = gameSession.Balance;
        gameSession.Balance -= amount.RoundToTwoDecimals();
        
        await context.SaveChangesAsync();
        
        logger.LogInformation(DecreaseBalanceMessage(gameSession.Id, amount, gameSession.Balance, oldBalance));
        
        return gameSession.Balance.RoundToTwoDecimals();
    }
}