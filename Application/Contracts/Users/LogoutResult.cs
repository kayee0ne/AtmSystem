namespace Contracts.Users;

public abstract record LogoutResult
{
    private LogoutResult() { }

    public record Success : LogoutResult;

    public record Failure : LogoutResult;
}