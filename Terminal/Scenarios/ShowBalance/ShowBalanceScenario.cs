using Contracts.Users;
using Spectre.Console;

namespace Terminal.Scenarios.ShowBalance;

public class ShowBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public ShowBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Show balance";

    public void Run()
    {
        decimal balance = _userService.GetBalance();
        AnsiConsole.MarkupLine($"Your balance is [bold]{balance}[/]");
        AnsiConsole.MarkupLine("Press [green]Enter[/] to continue");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}