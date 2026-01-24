using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.Services;

public class RandomProvider : IRandomProvider
{ 
    public double GetRandomNumberForWinChance() => Random.Shared.Next(1, 101);
    public decimal GetRandomMultiplierForSmallWin() => (decimal)(Random.Shared.NextDouble() * (2.0 - 0.1) + 0.1);
    public decimal GetRandomMultiplierForBigWin() => (decimal)(Random.Shared.NextDouble() * (10.0 - 2.0) + 2.0);
}