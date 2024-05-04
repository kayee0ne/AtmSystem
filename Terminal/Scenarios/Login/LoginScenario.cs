using Contracts.Users;
using Spectre.Console;

namespace Terminal.Scenarios.Login;

public class LoginScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Login";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Username:");

        string password = AnsiConsole.Ask<string>("Password:");

        LoginResult result = _userService.Login(username, password);

        switch (result)
        {
            case LoginResult.Success:
                AnsiConsole.MarkupLine("[bold green]Login successful[/]");
                break;
            case LoginResult.InvalidLogin:
                AnsiConsole.MarkupLine("[bold red]Invalid username[/]");
                break;
            case LoginResult.InvalidPassword:
                AnsiConsole.MarkupLine("[bold red]Invalid password[/]");
                break;
        }

        AnsiConsole.MarkupLine("[bold green]Press any key to continue[/]");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}