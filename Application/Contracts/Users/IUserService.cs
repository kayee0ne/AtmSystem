using Models.Operations;

namespace Contracts.Users;

public interface IUserService
{
    LoginResult Login(string username, string password);
    LogoutResult Logout();
    RegisterResult Register(string username, string password);
    WithdrawalResult Withdraw(decimal amount);
    DepositResult Deposit(decimal amount);
    IEnumerable<Operation> GetOperationsHistory();
    decimal GetBalance();
}