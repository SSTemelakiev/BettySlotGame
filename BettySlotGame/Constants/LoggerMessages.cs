namespace BettySlotGame.Constants;

public static class LoggerMessages
{
    #region Random Provider Service

    public static string GetRandomNumberForWinMessage(int roll) => $"RNG Win Chance Roll: {roll}";
    public static string GetRandomMultiplierForSmallWinMessage(decimal multiplier) => $"RNG Generated Small Win Multiplier: x{multiplier}";
    public static string GetRandomMultiplierForBigWinMessage(decimal multiplier) => $"RNG Generated BIG WIN Multiplier: x{multiplier}";

    #endregion

    #region Game Session Service

    public static string CreateGameSessionMessage(decimal balance) => $"Creating game session for balance {balance}";
    public static string SuccessfullyCreatedGameSessionMessage(int id) => $"Successfully created GameSession with Id: {id}";
    public static string FailedToCreateGameSessionMessage(string error) => $"Failed to create game session. Exception {error}";
    public static string GetGameSessionMessage(int gameSessionId) => $"Retrieving game session with Id: {gameSessionId}";
    public static string GameSessionNotFoundMessage(int gameSessionId) => $"Game session with {gameSessionId} was not found.";
    
    #endregion

    #region Balance Service

    public static string GetBalanceMessage(int gameSessionId, decimal balance) => $"Balance check for Session {gameSessionId}: {balance}";
    public static string IncreaseBalanceMessage(int gameSessionId, decimal amount, decimal balance, decimal oldBalance) => $"Balance increased for Session {gameSessionId}. Amount: {amount}. New Balance: {balance} (was {oldBalance})";
    public static string DecreaseBalanceMessage(int gameSessionId, decimal amount, decimal balance, decimal oldBalance) => $"Balance decreased for Session {gameSessionId}. Amount: {amount}. New Balance: {balance} (was {oldBalance})";

    #endregion
}