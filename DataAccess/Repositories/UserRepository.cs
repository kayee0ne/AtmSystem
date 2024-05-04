using Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Models.Users;
using Npgsql;
using NpgsqlTypes;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IServiceProvider postgresConnectionProvider)
    {
        _connectionString = postgresConnectionProvider.GetRequiredService<string>();
    }

    public User? FindUserByUsername(string username)
    {
        const string sql = "SELECT * FROM users WHERE username = @username";

        using var connection = new NpgsqlConnection(_connectionString);
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("username", username);

        connection.Open();
        using NpgsqlDataReader reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new User(
            reader.GetString(0),
            reader.GetString(1),
            Enum.Parse<UserRole>(reader.GetString(2)),
            reader.GetDecimal(3));
    }

    public void AddUser(User user)
    {
        const string sql =
            "INSERT INTO users (username, password, role, balance) VALUES (@username, @password, @role, @balance)";

        using var connection = new NpgsqlConnection(_connectionString);
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("username", NpgsqlDbType.Text, user.Username);
        command.Parameters.AddWithValue("password", NpgsqlDbType.Text, user.Password);
        command.Parameters.AddWithValue("role", NpgsqlDbType.Text, user.Role.ToString());
        command.Parameters.AddWithValue("balance", NpgsqlDbType.Numeric, user.Balance);

        connection.Open();

        command.ExecuteNonQuery();
    }

    public void UpdateUser(User user)
    {
        const string sql =
            "UPDATE users SET password = @password, role = @role, balance = @balance WHERE username = @username";

        using var connection = new NpgsqlConnection(_connectionString);
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("username", NpgsqlDbType.Text, user.Username);
        command.Parameters.AddWithValue("password", NpgsqlDbType.Text, user.Password);
        command.Parameters.AddWithValue("role", NpgsqlDbType.Text, user.Role.ToString());
        command.Parameters.AddWithValue("balance", NpgsqlDbType.Numeric, user.Balance);

        connection.Open();

        command.ExecuteNonQuery();
    }
}