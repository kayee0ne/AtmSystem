using Contracts.Users;
using Spectre.Console;

namespace Terminal.Scenarios.Withdrawal;

public class WithdrawalScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawalScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Withdrawal";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Amount:");
        WithdrawalResult result = _userService.Withdraw(amount);
        switch (result)
        {
            case WithdrawalResult.Success:
                AnsiConsole.MarkupLine("[bold green]Withdrawal successful[/]");
                break;
            case WithdrawalResult.InvalidAmount:
                AnsiConsole.MarkupLine("[bold red]Invalid amount[/]");
                break;
            case WithdrawalResult.InsufficientFunds:
                AnsiConsole.MarkupLine("[bold red]Insufficient funds[/]");
                break;
        }

        AnsiConsole.MarkupLine("[bold green]Press any key to continue[/]");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}