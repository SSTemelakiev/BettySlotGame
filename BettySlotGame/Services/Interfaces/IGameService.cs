namespace BettySlotGame.Services.Interfaces;

public interface IGameService
{
    public Task<int> CreateGame(decimal balance);
    public Task<decimal?> GetBalance(int gameId);
}