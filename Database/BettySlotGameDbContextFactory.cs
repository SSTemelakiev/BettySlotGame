using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database;

public class BettySlotGameDbContextFactory : IDesignTimeDbContextFactory<BettySlotGameDbContext>
{
    public BettySlotGameDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  
            .AddJsonFile("appsettings.json")
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<BettySlotGameDbContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        
        return new BettySlotGameDbContext(optionsBuilder.Options);
    }
}