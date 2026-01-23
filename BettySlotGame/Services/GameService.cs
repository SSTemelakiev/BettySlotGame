using BettySlotGame.Services.Interfaces;
using Database;
using Database.Models;

namespace BettySlotGame.Services;

public class GameService(BettySlotGameDbContext context) : IGameService
{
    public async Task<int> CreateGame(decimal balance)
    {
        var game = new GameEntity { Balance = balance };
        context.Games.Add(game);
        await context.SaveChangesAsync();
        return game.Id;
    }

    public async Task<decimal?> GetBalance(int gameId)
    {
        var game = await context.Games.FindAsync(gameId);
        return game?.Balance;
    }
}