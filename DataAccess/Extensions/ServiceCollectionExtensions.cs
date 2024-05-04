using Abstractions.Repositories;
using DataAccess.Repositories;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);
        collection.AddSingleton<string>("Host=localhost;Port=5432;Username=postgres;Password=kayee;Database=atm;");

        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IOperationRepository, OperationRepository>();

        return collection;
    }
}