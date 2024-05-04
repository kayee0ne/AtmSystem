using Microsoft.Extensions.DependencyInjection;
using Terminal.Scenarios.Deposit;
using Terminal.Scenarios.Login;
using Terminal.Scenarios.Logout;
using Terminal.Scenarios.OperationHistory;
using Terminal.Scenarios.Register;
using Terminal.Scenarios.ShowBalance;
using Terminal.Scenarios.Withdrawal;

namespace Terminal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, RegisterScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawalScenarioProvider>();
        collection.AddScoped<IScenarioProvider, OperationHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();

        return collection;
    }
}