namespace BettySlotGame.Services.Interfaces;

public interface IGameSessionService
{
    public int CreateGameSession(decimal balance);
    public decimal GetBalance(int gameSessionId);
    public decimal IncreaseBalance(int gameSessionId, decimal amount);
    public decimal DecreaseBalance(int gameSessionId, decimal amount);

}