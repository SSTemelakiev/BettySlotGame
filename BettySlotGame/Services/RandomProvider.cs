using BettySlotGame.Services.Interfaces;
using Microsoft.Extensions.Logging;
using static BettySlotGame.Constants.LoggerMessages;

namespace BettySlotGame.Services;

public class RandomProvider(ILogger<RandomProvider> logger) : IRandomProvider
{
    public double GetRandomNumberForWinChance()
    {
        var winChance = Random.Shared.Next(1, 101);

        logger.LogDebug(GetRandomNumberForWinMessage(winChance));
        
        return winChance;
    }

    public decimal GetRandomMultiplierForSmallWin()
    {
        var multiplier = (decimal)(Random.Shared.NextDouble() * (2.0 - 0.1) + 0.1);
        
        logger.LogInformation(GetRandomMultiplierForSmallWinMessage(multiplier));
        
        return multiplier;
    } 
    public decimal GetRandomMultiplierForBigWin()
    {
        var multiplier = (decimal)(Random.Shared.NextDouble() * (10.0 - 2.0) + 2.0);
        
        logger.LogInformation(GetRandomMultiplierForBigWinMessage(multiplier));
        
        return multiplier;
    }
}