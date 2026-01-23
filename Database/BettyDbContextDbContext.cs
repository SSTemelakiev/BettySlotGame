using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class BettySlotGameDbContext(DbContextOptions<BettySlotGameDbContext> options) : DbContext(options)
{
    public DbSet<GameEntity> Games { get; set; }
}