using Contracts.Users;
using Spectre.Console;

namespace Terminal.Scenarios.Register;

public class RegisterScenario : IScenario
{
    private readonly IUserService _userService;

    public RegisterScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Register";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Username:");
        string password = AnsiConsole.Ask<string>("Password:");

        RegisterResult result = _userService.Register(username, password);

        switch (result)
        {
            case RegisterResult.Success:
                AnsiConsole.MarkupLine("[bold green]Registration successful[/]");
                break;
            case RegisterResult.UsernameTaken:
                AnsiConsole.MarkupLine("[bold red]Username is already taken[/]");
                break;
            case RegisterResult.InvalidPassword:
                AnsiConsole.MarkupLine("[bold red]Password is invalid(Password must be 4 or more characters)[/]");
                break;
            case RegisterResult.InvalidUsername:
                AnsiConsole.MarkupLine("[bold red]Username is invalid(Username must be 4 or more characters)[/]");
                break;
        }

        AnsiConsole.MarkupLine("Press [green]Enter[/] to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}