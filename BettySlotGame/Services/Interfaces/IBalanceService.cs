using Database.Models;

namespace BettySlotGame.Services.Interfaces;

public interface IBalanceService
{
    public decimal GetBalance(GameSessionEntity gameSession);
    public decimal IncreaseBalance(GameSessionEntity gameSession, decimal amount);
    public decimal DecreaseBalance(GameSessionEntity gameSession, decimal amount);
}