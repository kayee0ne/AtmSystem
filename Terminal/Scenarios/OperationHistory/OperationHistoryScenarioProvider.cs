using System.Diagnostics.CodeAnalysis;
using Contracts.Users;

namespace Terminal.Scenarios.OperationHistory;

public class OperationHistoryScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public OperationHistoryScenarioProvider(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User is not null)
        {
            scenario = new OperationHistoryScenario(_userService);
            return true;
        }

        scenario = null;
        return false;
    }
}