namespace BettySlotGame.Extensions;

public static class DecimalExtension
{
    public static decimal RoundToTwoDecimals(this decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}