using Moq;
using Xunit;
using BettySlotGame.GameOperations;
using BettySlotGame.Services;
using BettySlotGame.Services.Interfaces;
using BettySlotGame.Constants;
using Database.Models;

namespace Tests.GameOperationsTests;

public class WithdrawTests
{
    private readonly Mock<IGameSessionService> _gameSessionServiceMock;
    private readonly Mock<IBalanceService> _balanceServiceMock;
    private readonly GameStateService _gameStateService;
    private readonly Withdraw _withdrawOperation;

    public WithdrawTests()
    {
        _gameSessionServiceMock = new Mock<IGameSessionService>();
        _balanceServiceMock = new Mock<IBalanceService>();
        _gameStateService = new GameStateService();
        
        _withdrawOperation = new Withdraw(
            _gameStateService, 
            _gameSessionServiceMock.Object, 
            _balanceServiceMock.Object);
    }

    [Fact]
    public async Task ProcessOperation_ShouldReturnError_WhenAmountIsZeroOrLess()
    {
        var result = await _withdrawOperation.ProcessOperation(0);
        
        Assert.Equal(DisplayMessages.AmountMustBePositiveMessage, result);
    }

    [Fact]
    public async Task ProcessOperation_ShouldReturnInsufficientFunds_WhenBalanceIsTooLow()
    {
        int sessionId = 1;
        _gameStateService.CurrentSessionId = sessionId;
        var mockSession = new GameSessionEntity { Id = sessionId, Balance = 50 };

        _gameSessionServiceMock.Setup(s => s.GetGameSession(sessionId)).ReturnsAsync(mockSession);
        
        _balanceServiceMock.Setup(b => b.GetBalance(mockSession)).ReturnsAsync(50);
        
        var result = await _withdrawOperation.ProcessOperation(100);
        
        Assert.Equal(DisplayMessages.InsufficientFundsForWithdrawalMessage, result);
    }

    [Fact]
    public async Task ProcessOperation_ShouldReturnSuccess_WhenBalanceIsSufficient()
    {
        // Arrange
        int sessionId = 1;
        decimal withdrawAmount = 40;
        decimal initialBalance = 100;
        decimal finalBalance = 60;
        
        _gameStateService.CurrentSessionId = sessionId;
        var mockSession = new GameSessionEntity { Id = sessionId, Balance = initialBalance };

        _gameSessionServiceMock.Setup(s => s.GetGameSession(sessionId)).ReturnsAsync(mockSession);
        _balanceServiceMock.Setup(b => b.GetBalance(mockSession)).ReturnsAsync(initialBalance);
        _balanceServiceMock.Setup(b => b.DecreaseBalance(mockSession, withdrawAmount)).ReturnsAsync(finalBalance);
        
        var result = await _withdrawOperation.ProcessOperation(withdrawAmount);
        
        var expectedMessage = DisplayMessages.SuccessWithdrawalMessage(withdrawAmount, finalBalance);
        Assert.Equal(expectedMessage, result);
    }
}