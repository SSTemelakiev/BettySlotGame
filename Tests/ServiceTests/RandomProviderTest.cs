using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using BettySlotGame.Services;

namespace Tests.ServiceTests;

public class RandomProviderTests
{
    private readonly Mock<ILogger<RandomProvider>> _loggerMock;
    private readonly RandomProvider _randomProvider;

    public RandomProviderTests()
    {
        _loggerMock = new Mock<ILogger<RandomProvider>>();
        _randomProvider = new RandomProvider(_loggerMock.Object);
    }

    [Fact]
    public void GetRandomNumberForWinChance_ShouldReturnNumberBetween1And100()
    {
        for (int i = 0; i < 1000; i++)
        {
            var result = _randomProvider.GetRandomNumberForWinChance();
            Assert.InRange(result, 1, 100);
        }
    }

    [Fact]
    public void GetRandomMultiplierForSmallWin_ShouldReturnNumberWithinSmallWinRange()
    {
        for (int i = 0; i < 1000; i++)
        {
            var result = _randomProvider.GetRandomMultiplierForSmallWin();
            
            Assert.True(result >= 0.1m && result < 2, 
                $"Multiplier {result} was outside expected range [0.1, 2)");
        }
    }

    [Fact]
    public void GetRandomMultiplierForBigWin_ShouldReturnNumberWithinBigWinRange()
    {
        for (int i = 0; i < 1000; i++)
        {
            var result = _randomProvider.GetRandomMultiplierForBigWin();
            
            Assert.True(result >= 2 && result < 10, 
                $"Multiplier {result} was outside expected range [2, 10)");
        }
    }
}