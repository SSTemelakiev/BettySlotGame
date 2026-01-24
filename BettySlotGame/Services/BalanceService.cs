using BettySlotGame.Extensions;
using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;
using Microsoft.Extensions.Logging;
using static BettySlotGame.Constants.LoggerMessages;

namespace BettySlotGame.Services;

public class BalanceService(BettySlotGameDbContext context, ILogger<BalanceService> logger) : IBalanceService
{
    public decimal GetBalance(GameSessionEntity gameSession)
    {
        var balance = gameSession.Balance.RoundToTwoDecimals();
        logger.LogDebug(GetBalanceMessage(gameSession.Id, balance));
        return balance;
    } 

    public decimal IncreaseBalance(GameSessionEntity gameSession, decimal amount)
    {
        var oldBalance = gameSession.Balance;
        gameSession.Balance += amount.RoundToTwoDecimals();
        
        context.SaveChanges();
        
        logger.LogInformation(IncreaseBalanceMessage(gameSession.Id, amount, gameSession.Balance, oldBalance));
        
        return gameSession.Balance.RoundToTwoDecimals();
    }
    
    public decimal DecreaseBalance(GameSessionEntity gameSession, decimal amount)
    {
        var oldBalance = gameSession.Balance;
        gameSession.Balance -= amount.RoundToTwoDecimals();
        
        context.SaveChanges();
        
        logger.LogInformation(DecreaseBalanceMessage(gameSession.Id, amount, gameSession.Balance, oldBalance));
        
        return gameSession.Balance.RoundToTwoDecimals();
    }
}