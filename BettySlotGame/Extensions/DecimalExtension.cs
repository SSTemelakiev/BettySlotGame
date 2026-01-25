namespace BettySlotGame.Extensions;

public static class DecimalExtension
{
    public static decimal RoundToTwoDecimals(this decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}