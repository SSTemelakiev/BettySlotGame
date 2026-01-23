using BettySlotGame.Services.Interfaces;

namespace BettySlotGame.Services;

public class RandomProvider : IRandomProvider
{ 
    private readonly Random _random = new();
    
    public double GetRandomNumber() => _random.NextDouble();
}