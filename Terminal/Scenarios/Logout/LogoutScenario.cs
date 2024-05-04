using Contracts.Users;
using Spectre.Console;

namespace Terminal.Scenarios.Logout;

public class LogoutScenario : IScenario
{
    private readonly IUserService _userService;

    public LogoutScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Logout";

    public void Run()
    {
        LogoutResult result = _userService.Logout();
        switch (result)
        {
            case LogoutResult.Success:
                AnsiConsole.MarkupLine("[bold green]Logout successful[/]");
                break;
            case LogoutResult.Failure:
                AnsiConsole.MarkupLine("[bold red]You are not logged in[/]");
                break;
        }

        AnsiConsole.MarkupLine("Press [green]Enter[/] to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}