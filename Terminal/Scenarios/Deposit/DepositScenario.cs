using Contracts.Users;
using Spectre.Console;

namespace Terminal.Scenarios.Deposit;

public class DepositScenario : IScenario
{
    private readonly IUserService _userService;

    public DepositScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Deposit";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Amount:");
        DepositResult result = _userService.Deposit(amount);
        switch (result)
        {
            case DepositResult.Success:
                AnsiConsole.MarkupLine("[bold green]Deposit successful[/]");
                break;
            case DepositResult.InvalidAmount:
                AnsiConsole.MarkupLine("[bold red]Invalid amount[/]");
                break;
        }

        AnsiConsole.MarkupLine("[bold green]Press any key to continue[/]");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}