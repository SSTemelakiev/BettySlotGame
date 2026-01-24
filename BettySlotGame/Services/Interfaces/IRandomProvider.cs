namespace BettySlotGame.Services.Interfaces;

public interface IRandomProvider
{
    double GetRandomNumberForWinChance();
    decimal GetRandomMultiplierForSmallWin();
    decimal GetRandomMultiplierForBigWin();
}