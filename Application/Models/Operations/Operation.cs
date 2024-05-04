namespace Models.Operations;

public record Operation(
    string Username,
    decimal Amount,
    DateTime Timestamp,
    decimal Balance,
    decimal BalanceAfterOperation,
    OperationType Type);