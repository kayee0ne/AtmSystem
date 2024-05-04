namespace Contracts.Users;

public abstract record WithdrawalResult
{
    private WithdrawalResult() { }

    public sealed record Success : WithdrawalResult;

    public sealed record InsufficientFunds : WithdrawalResult;

    public sealed record InvalidAmount : WithdrawalResult;
}