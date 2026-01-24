using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Database;
using Database.Models;
using BettySlotGame.Services;

namespace Tests.ServiceTests;

public class BalanceServiceTests : IDisposable
{
    private readonly BettySlotGameDbContext _context;
    private readonly Mock<ILogger<BalanceService>> _loggerMock;
    private readonly BalanceService _balanceService;

    public BalanceServiceTests()
    {
        var options = new DbContextOptionsBuilder<BettySlotGameDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BettySlotGameDbContext(options);
        _loggerMock = new Mock<ILogger<BalanceService>>();
        _balanceService = new BalanceService(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task IncreaseBalance_ShouldUpdateDatabaseAndReturnNewValue()
    {
        var session = new GameSessionEntity { Id = 1, Balance = 10, StartedAt = DateTime.UtcNow };
        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        var result = await _balanceService.IncreaseBalance(session, 5);

        Assert.Equal(15, result);
        
        var dbSession = await _context.GameSessions.FindAsync(1);
        Assert.Equal(15, dbSession!.Balance);
    }

    [Fact]
    public async Task DecreaseBalance_ShouldSubtractFromBalanceCorrectly()
    {
        var session = new GameSessionEntity { Id = 2, Balance = 100, StartedAt = DateTime.UtcNow };
        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        var result = await _balanceService.DecreaseBalance(session, 40);

        Assert.Equal(60, result);
        Assert.Equal(60, session.Balance);
    }

    [Fact]
    public async Task GetBalance_ShouldReturnRoundedValue()
    {
        var session = new GameSessionEntity { Id = 3, Balance = 10.12345m };

        var result = await _balanceService.GetBalance(session);

        Assert.Equal(10.12m, result);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}