namespace Models.Users;

public class User
{
    public User(string username, string password, UserRole role, decimal balance)
    {
        Username = username;
        Password = password;
        Role = role;
        Balance = balance;
    }

    public string Username { get; }
    public string Password { get; }
    public UserRole Role { get; }
    public decimal Balance { get; set; }
}