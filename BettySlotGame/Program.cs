using BettySlotGame;
using BettySlotGame.extensions;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) => { services.AddServices(context.Configuration); })
    .Build();

await Game.Start(host);