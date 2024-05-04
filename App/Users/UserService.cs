using Abstractions.Repositories;
using Contracts.Users;
using Models.Operations;
using Models.Users;

namespace Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly CurrentUserManager _currentUserManager;

    public UserService(
        IUserRepository userRepository,
        IOperationRepository operationRepository,
        CurrentUserManager currentUserManager)
    {
        _userRepository = userRepository;
        _operationRepository = operationRepository;
        _currentUserManager = currentUserManager;
    }

    public LoginResult Login(string username, string password)
    {
        User? user = _userRepository.FindUserByUsername(username);
        if (user == null)
        {
            return new LoginResult.InvalidLogin();
        }

        if (user.Password != password)
        {
            return new LoginResult.InvalidPassword();
        }

        _currentUserManager.User = user;
        return new LoginResult.Success();
    }

    public LogoutResult Logout()
    {
        if (_currentUserManager.User == null)
        {
            return new LogoutResult.Failure();
        }

        _currentUserManager.User = null;
        return new LogoutResult.Success();
    }

    public RegisterResult Register(string username, string password)
    {
        if (username.Length < 3)
        {
            return new RegisterResult.InvalidUsername("Username is too short");
        }

        if (password.Length < 3)
        {
            return new RegisterResult.InvalidPassword("Password is too short");
        }

        User? user = _userRepository.FindUserByUsername(username);
        if (user != null)
        {
            return new RegisterResult.UsernameTaken();
        }

        _userRepository.AddUser(new User(username, password, UserRole.User, 0));
        return new RegisterResult.Success();
    }

    public WithdrawalResult Withdraw(decimal amount)
    {
        if (_currentUserManager.User == null)
        {
            throw new InvalidOperationException("User is not logged in");
        }

        if (_currentUserManager.User.Balance < amount)
        {
            return new WithdrawalResult.InsufficientFunds();
        }

        if (amount <= 0)
        {
            return new WithdrawalResult.InvalidAmount();
        }

        decimal balance = _currentUserManager.User.Balance;
        _currentUserManager.User.Balance -= amount;

        string username = _currentUserManager.User.Username;
        DateTime timestamp = DateTime.UtcNow;
        decimal balanceAfterOperation = _currentUserManager.User.Balance;
        const OperationType type = OperationType.Withdrawal;
        var operation = new Operation(username, amount, timestamp, balance, balanceAfterOperation, type);

        _operationRepository.AddOperation(operation);
        _userRepository.UpdateUser(_currentUserManager.User);

        return new WithdrawalResult.Success();
    }

    public DepositResult Deposit(decimal amount)
    {
        if (_currentUserManager.User == null)
        {
            throw new InvalidOperationException("User is not logged in");
        }

        decimal balance = _currentUserManager.User.Balance;
        _currentUserManager.User.Balance += amount;

        string username = _currentUserManager.User.Username;
        DateTime timestamp = DateTime.UtcNow;
        decimal balanceAfterOperation = _currentUserManager.User.Balance;
        const OperationType type = OperationType.Deposit;

        var operation = new Operation(username, amount, timestamp, balance, balanceAfterOperation, type);
        _userRepository.UpdateUser(_currentUserManager.User);

        _operationRepository.AddOperation(operation);

        return new DepositResult.Success();
    }

    public IEnumerable<Operation> GetOperationsHistory()
    {
        if (_currentUserManager.User == null)
        {
            throw new InvalidOperationException("User is not logged in");
        }

        return _operationRepository.GetOperationHistoryByUsername(_currentUserManager.User.Username);
    }

    public decimal GetBalance()
    {
        if (_currentUserManager.User == null)
        {
            throw new InvalidOperationException("User is not logged in");
        }

        User? user = _userRepository.FindUserByUsername(_currentUserManager.User.Username);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return user.Balance;
    }
}