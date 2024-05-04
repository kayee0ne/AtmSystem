using Abstractions.Repositories;
using Application.Users;
using Contracts.Users;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IOperationRepository, OperationRepository>();

        collection.AddScoped<CurrentUserManager>();
        collection.AddScoped<ICurrentUserService>(p => p.GetRequiredService<CurrentUserManager>());

        return collection;
    }
}