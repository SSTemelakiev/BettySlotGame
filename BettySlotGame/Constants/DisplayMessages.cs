namespace BettySlotGame.Constants;

public static class DisplayMessages
{
    #region Input Processor

    public static string InputProcessorReadActionMessage => "Please enter a valid action.";
    public static string InputProcessorReadAmountMessage => "Please enter a valid positive amount.";

    #endregion

    #region Game Session Service

    public static string GameNotFoundMessage(int gameId) => $"Game with {gameId} was not found.";

    #endregion

    #region Game

    public static string SubmitActionMessage => "Please submit action:";
    public static string EndGameMessage => "Thank you for playing! Hope to see you again soon.";
    public static string UnknownCommandMessage(string? command) => $"Unknown command: {command}";
    public static string ExceptionMessage(string exceptionMessage) => $"Error: {exceptionMessage}";

    #endregion
    
    #region Bet
    
    public static string BalanceLessThanMinimumBetAmount => "Insufficient funds. Your balance is less than minimum bet betAmount of $1.";
    public static string InsufficientFundsMessage => "Insufficient funds. BetAmount betAmount must be less than your current balance.";
    public static string InvalidBetAmountMessage => "BetAmount betAmount must be between $1 and $10.";
    public static string LoseMessage(decimal balance) => $"No luck this time! Your current balance is  ${balance}";
    public static string WinMessage(decimal winAmount, decimal balance) => $"Congrats - you won ${winAmount}! Your current balance is  ${balance}";
    
    #endregion

    #region Deposit

    public static string AmountMustBePositiveMessage => "Amount must be positive.";
    public static string SuccessDepositMessage(decimal depositAmount, decimal balance) => $"Your deposit of ${depositAmount} was successful. Your current balance is: ${balance}";

    #endregion
    
    #region Withdrawal
    
    public static string InsufficientFundsForWithdrawalMessage => "Insufficient funds for withdrawal.";
    public static string SuccessWithdrawalMessage(decimal withdrawalAmount, decimal balance) => $"Your withdrawal of ${withdrawalAmount} was successful. Your current balance is: ${balance}";
    
    #endregion
}