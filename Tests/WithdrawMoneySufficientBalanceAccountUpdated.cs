using Abstractions.Repositories;
using Application.Users;
using Contracts.Users;
using Models.Users;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class WithdrawMoneySufficientBalanceAccountUpdated
{
    [Fact]
    public void Run()
    {
        var user = new User("test", "test", UserRole.User, 100);
        IUserRepository? userRepository = Substitute.For<IUserRepository>();
        userRepository.FindUserByUsername("test").Returns(user);
        IOperationRepository? operationRepository = Substitute.For<IOperationRepository>();
        var currentUserManager = new CurrentUserManager
        {
            User = user,
        };
        var userService = new UserService(userRepository, operationRepository, currentUserManager);

        WithdrawalResult withdrawalResult = userService.Withdraw(50);

        Assert.IsType<WithdrawalResult.Success>(withdrawalResult);
        Assert.Equal(50, user.Balance);
    }
}