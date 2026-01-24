using BettySlotGame.Extensions;
using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;

namespace BettySlotGame.Services;

public class BalanceService(BettySlotGameDbContext context) : IBalanceService
{
    public decimal GetBalance(GameSessionEntity gameSession) => gameSession.Balance.RoundToTwoDecimals();

    public decimal IncreaseBalance(GameSessionEntity gameSession, decimal amount)
    {
        gameSession.Balance += amount.RoundToTwoDecimals();
        context.SaveChanges();
        return gameSession.Balance.RoundToTwoDecimals();
    }
    
    public decimal DecreaseBalance(GameSessionEntity gameSession, decimal amount)
    {
        gameSession.Balance -= amount.RoundToTwoDecimals();
        context.SaveChanges();
        return gameSession.Balance.RoundToTwoDecimals();
    }
}