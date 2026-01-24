using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Database;
using Database.Models;
using BettySlotGame.Services;

namespace Tests.ServiceTests;

public class GameSessionServiceTests : IDisposable
{
    private readonly BettySlotGameDbContext _context;
    private readonly Mock<ILogger<GameSessionService>> _loggerMock;
    private readonly GameSessionService _service;

    public GameSessionServiceTests()
    {
        var options = new DbContextOptionsBuilder<BettySlotGameDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BettySlotGameDbContext(options);
        _loggerMock = new Mock<ILogger<GameSessionService>>();
        _service = new GameSessionService(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateGameSession_ShouldSaveToDatabase_AndReturnNewId()
    {
        decimal initialBalance = 100.506m; 

        var resultId = await _service.CreateGameSession(initialBalance);

        Assert.True(resultId > 0);
        
        var savedSession = await _context.GameSessions.FindAsync(resultId);
        Assert.NotNull(savedSession);
        Assert.Equal(100.51m, savedSession.Balance);
    }

    [Fact]
    public async Task GetGameSession_ShouldReturnSession_WhenIdExists()
    {
        var session = new GameSessionEntity { Id = 99, Balance = 50 };
        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        var result = await _service.GetGameSession(99);

        Assert.NotNull(result);
        Assert.Equal(99, result.Id);
        Assert.Equal(50, result.Balance);
    }

    [Fact]
    public async Task GetGameSession_ShouldThrowKeyNotFoundException_WhenIdDoesNotExist()
    {
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _service.GetGameSession(404));

        Assert.Contains("404", exception.Message);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}