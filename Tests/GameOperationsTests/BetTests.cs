using Moq;
using Xunit;
using BettySlotGame.GameOperations;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using BettySlotGame.Constants;
using Database.Models;

namespace Tests.GameOperationsTests;

public class BetTests
{
    private readonly Mock<IGameSessionService> _gameSessionServiceMock;
    private readonly Mock<IBalanceService> _balanceServiceMock;
    private readonly Mock<IRandomProvider> _randomProviderMock;
    private readonly GameStateService _gameStateService;
    private readonly Bet _betOperation;

    public BetTests()
    {
        _gameSessionServiceMock = new Mock<IGameSessionService>();
        _balanceServiceMock = new Mock<IBalanceService>();
        _randomProviderMock = new Mock<IRandomProvider>();
        _gameStateService = new GameStateService { CurrentSessionId = 1 };
        
        _betOperation = new Bet(
            _randomProviderMock.Object,
            _gameStateService, 
            _gameSessionServiceMock.Object, 
            _balanceServiceMock.Object);
    }

    [Theory]
    [InlineData(0.5)]  
    [InlineData(11)]
    public async Task ProcessOperation_ShouldReturnInvalidBet_WhenAmountIsOutOfRange(decimal amount)
    {
        var mockSession = new GameSessionEntity { Id = 1, Balance = 100 };
        _gameSessionServiceMock.Setup(s => s.GetGameSession(1)).ReturnsAsync(mockSession);
        _balanceServiceMock.Setup(b => b.GetBalance(mockSession)).ReturnsAsync(100);
        
        var result = await _betOperation.ProcessOperation(amount);
        
        Assert.Equal(DisplayMessages.InvalidBetAmountMessage, result);
    }

    [Fact]
    public async Task ProcessOperation_ShouldHandleLoss_WhenWinChanceIs50OrLess()
    {
        var mockSession = new GameSessionEntity { Id = 1, Balance = 10 };
        _gameSessionServiceMock.Setup(s => s.GetGameSession(1)).ReturnsAsync(mockSession);
        _balanceServiceMock.Setup(b => b.GetBalance(mockSession)).ReturnsAsync(10);
        
        _randomProviderMock.Setup(r => r.GetRandomNumberForWinChance()).Returns(40); 
        _balanceServiceMock.Setup(b => b.DecreaseBalance(mockSession, 2)).ReturnsAsync(8);

        var result = await _betOperation.ProcessOperation(2);

        Assert.Equal(DisplayMessages.LoseMessage(8), result);
    }

    [Fact]
    public async Task ProcessOperation_ShouldHandleBigWin_WhenWinChanceIsOver90()
    {
        var mockSession = new GameSessionEntity { Id = 1, Balance = 10 };
        decimal bet = 5;
        decimal multiplier = 10;
        
        _gameSessionServiceMock.Setup(s => s.GetGameSession(1)).ReturnsAsync(mockSession);
        _balanceServiceMock.Setup(b => b.GetBalance(mockSession)).ReturnsAsync(10);
        
        _randomProviderMock.Setup(r => r.GetRandomNumberForWinChance()).Returns(95);
        _randomProviderMock.Setup(r => r.GetRandomMultiplierForBigWin()).Returns(multiplier);
        
        _balanceServiceMock.Setup(b => b.DecreaseBalance(mockSession, bet)).ReturnsAsync(5);
        _balanceServiceMock.Setup(b => b.IncreaseBalance(mockSession, 50)).ReturnsAsync(55);

        var result = await _betOperation.ProcessOperation(bet);

        Assert.Equal(DisplayMessages.WinMessage(50, 55), result);
    }
}