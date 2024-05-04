using Application.Extensions;
using DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Terminal;
using Terminal.Extensions;

var collection = new ServiceCollection();

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 5432;
        configuration.Username = "postgres";
        configuration.Password = "kayee";
        configuration.Database = "atm";
        configuration.SslMode = "Prefer";
    }).AddPresentationConsole();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

scope.UseInfrastructureDataAccess();

ScenarioRunner runner = scope.ServiceProvider.GetRequiredService<ScenarioRunner>();

while (true)
{
    runner.Run();
    AnsiConsole.Clear();
}