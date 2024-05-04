using System.Diagnostics.CodeAnalysis;
using Contracts.Users;

namespace Terminal.Scenarios.Withdrawal;

public class WithdrawalScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public WithdrawalScenarioProvider(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User is not null)
        {
            scenario = new WithdrawalScenario(_userService);
            return true;
        }

        scenario = null;
        return false;
    }
}