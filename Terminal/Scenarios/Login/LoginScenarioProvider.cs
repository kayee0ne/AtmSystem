using System.Diagnostics.CodeAnalysis;
using Contracts.Users;

namespace Terminal.Scenarios.Login;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public LoginScenarioProvider(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User is null)
        {
            scenario = new LoginScenario(_userService);
            return true;
        }

        scenario = null;
        return false;
    }
}