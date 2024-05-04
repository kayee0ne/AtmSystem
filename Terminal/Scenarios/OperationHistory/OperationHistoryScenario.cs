using System.Globalization;
using Contracts.Users;
using Models.Operations;
using Spectre.Console;

namespace Terminal.Scenarios.OperationHistory;

public class OperationHistoryScenario : IScenario
{
    private readonly IUserService _userService;

    public OperationHistoryScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name { get; } = "Operation History";

    [Obsolete("Obsolete")]
    public void Run()
    {
        IEnumerable<Operation> operations = _userService.GetOperationsHistory();
        IEnumerable<Operation> enumerable = operations.ToList();

        if (!enumerable.Any())
        {
            AnsiConsole.MarkupLine("[bold red]No operations found[/]");

            AnsiConsole.MarkupLine("[bold green]Press any key to continue[/]");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        var table = new Table();
        table.AddColumn("Type");
        table.AddColumn("Amount");
        table.AddColumn("Date|Time");
        table.AddColumn("Balance");
        table.AddColumn("Balance after operation");
        foreach (Operation operation in enumerable)
        {
            table.AddRow(
                operation.Type.ToString(),
                operation.Amount.ToString("C", CultureInfo.InvariantCulture),
                operation.Timestamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                operation.Balance.ToString("C", CultureInfo.InvariantCulture),
                operation.BalanceAfterOperation.ToString("C", CultureInfo.InvariantCulture));
        }

        AnsiConsole.Render(table);

        AnsiConsole.MarkupLine("[bold green]Operations displayed[/]");

        AnsiConsole.MarkupLine("[bold green]Press any key to continue[/]");
        AnsiConsole.Console.Input.ReadKey(false);
    }
}