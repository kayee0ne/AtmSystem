namespace Contracts.Users;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record InvalidPassword : LoginResult;

    public sealed record InvalidLogin : LoginResult;
}