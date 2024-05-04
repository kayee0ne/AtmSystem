namespace Contracts.Users;

public abstract record RegisterResult
{
    private RegisterResult() { }

    public sealed record Success : RegisterResult;

    public sealed record UsernameTaken : RegisterResult;

    public sealed record InvalidUsername(string Comment) : RegisterResult;

    public sealed record InvalidPassword(string Comment) : RegisterResult;
}