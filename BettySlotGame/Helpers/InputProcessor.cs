using System.Globalization;
using BettySlotGame.Extensions;

namespace BettySlotGame.Helpers;

public static class InputProcessor
{
    public static string ReadAction(string? input)
    {
        while (true)
        {
            var inputParts = input?.Split(' ');
            if (inputParts != null && inputParts.Length > 0)
            {
                return inputParts[0];
            }

            Console.WriteLine("Please enter a valid action.");
            input = Console.ReadLine();
        }
    }

    public static decimal ReadAmount(string? input)
    {
        while (true)
        {
            var inputParts = input?.Split(' ').Skip(1).ToArray();
            if (inputParts != null && inputParts.Length > 0 && TryParseAmount(inputParts[0], out var amount) &&
                amount > 0)
            {
                return amount.RoundToTwoDecimals();
            }

            Console.WriteLine("Please enter a valid positive amount.");
            input = Console.ReadLine();
        }
    }
    
    private static bool TryParseAmount(string? input, out decimal amount)
    {
        return decimal.TryParse(
            input,
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            CultureInfo.InvariantCulture,
            out amount);
    }
}