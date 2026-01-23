using Database;

var factory = new BettySlotGameDbContextFactory();
using var db = factory.CreateDbContext(args);

db.Database.EnsureCreated();
Console.WriteLine("Successfully connected using appsettings.json!");