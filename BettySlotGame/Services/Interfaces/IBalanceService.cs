using Database.Models;

namespace BettySlotGame.Services.Interfaces;

public interface IBalanceService
{
    public Task<decimal> GetBalance(GameSessionEntity gameSession);
    public Task<decimal> IncreaseBalance(GameSessionEntity gameSession, decimal amount);
    public Task<decimal> DecreaseBalance(GameSessionEntity gameSession, decimal amount);
}