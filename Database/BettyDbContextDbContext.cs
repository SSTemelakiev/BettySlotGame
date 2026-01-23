using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class BettySlotGameDbContext(DbContextOptions<BettySlotGameDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<GameSessionEntity>()
            .Property(g => g.Balance)
            .HasPrecision(18, 2);
    }

    public DbSet<GameSessionEntity> GameSessions { get; set; }
}