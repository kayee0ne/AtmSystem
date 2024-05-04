using Models.Users;

namespace Abstractions.Repositories;

public interface IUserRepository
{
    User? FindUserByUsername(string username);
    void AddUser(User user);
    void UpdateUser(User user);
}