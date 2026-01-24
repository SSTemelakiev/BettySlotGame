using BettySlotGame.Constants;
using BettySlotGame.GameOperations;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using Database.Models;
using Moq;
using Xunit;

namespace Tests.GameOperationsTests;

public class DepositTests
{
    private readonly Mock<IGameSessionService> _gameSessionServiceMock;
    private readonly Mock<IBalanceService> _balanceServiceMock;
    private readonly GameStateService _gameStateService;
    private readonly Deposit _depositOperation;
    
    public DepositTests()
    {
        _gameSessionServiceMock = new Mock<IGameSessionService>();
        _balanceServiceMock = new Mock<IBalanceService>();
        
        _gameStateService = new GameStateService();
        
        _depositOperation = new Deposit(
            _gameStateService, 
            _gameSessionServiceMock.Object, 
            _balanceServiceMock.Object);
    }
    
    [Fact]
    public async Task ProcessOperation_ShouldReturnErrorMessage_WhenAmountIsZeroOrLess()
    {
        decimal invalidAmount = -10;
        
        var result = await _depositOperation.ProcessOperation(invalidAmount);
        
        Assert.Equal(DisplayMessages.AmountMustBePositiveMessage, result);
    }
    
    [Fact]
    public async Task ProcessOperation_ShouldReturnSuccessMessage_WhenDepositIsValid()
    {
        int sessionId = 1;
        decimal depositAmount = 100;
        decimal expectedBalance = 100;
        _gameStateService.CurrentSessionId = sessionId;

        var mockSession = new GameSessionEntity { Id = sessionId, Balance = 0 };

        _gameSessionServiceMock
            .Setup(s => s.GetGameSession(sessionId))
            .ReturnsAsync(mockSession);

        _balanceServiceMock
            .Setup(b => b.IncreaseBalance(mockSession, depositAmount))
            .ReturnsAsync(expectedBalance);
        
        var result = await _depositOperation.ProcessOperation(depositAmount);
        
        var expectedMessage = DisplayMessages.SuccessDepositMessage(depositAmount, expectedBalance);
        Assert.Equal(expectedMessage, result);
    }
    
    [Fact]
    public async Task ProcessOperation_ShouldThrowException_WhenSessionDoesNotExist()
    {
        _gameStateService.CurrentSessionId = 99;
        _gameSessionServiceMock
            .Setup(s => s.GetGameSession(99))
            .ThrowsAsync(new KeyNotFoundException());
        
        await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _depositOperation.ProcessOperation(50));
    }
}