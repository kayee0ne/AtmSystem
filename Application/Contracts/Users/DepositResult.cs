namespace Contracts.Users;

public abstract record DepositResult
{
    private DepositResult() { }

    public sealed record Success : DepositResult;

    public sealed record InvalidAmount : DepositResult;
}