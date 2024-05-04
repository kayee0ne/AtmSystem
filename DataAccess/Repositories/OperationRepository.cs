using Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Models.Operations;
using Npgsql;
using NpgsqlTypes;

namespace DataAccess.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly string _connectionString;

    public OperationRepository(IServiceProvider connectionProvider)
    {
        _connectionString = connectionProvider.GetRequiredService<string>();
    }

    public IEnumerable<Operation> GetOperationHistoryByUsername(string username)
    {
        const string sql = "SELECT * FROM operations WHERE username = @username";

        using var connection = new NpgsqlConnection(_connectionString);
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("username", NpgsqlDbType.Text, username);

        connection.Open();

        using NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new Operation(
                reader.GetFieldValue<string>(0),
                reader.GetFieldValue<decimal>(1),
                reader.GetFieldValue<DateTime>(2),
                reader.GetFieldValue<decimal>(3),
                reader.GetFieldValue<decimal>(4),
                Enum.Parse<OperationType>(reader.GetFieldValue<string>(5)));
        }
    }

    public void AddOperation(Operation operation)
    {
        const string sql =
            "INSERT INTO operations (username, amount, timestamp, balance, balance_after_operation, type) " +
            "VALUES (@username, @amount, @timestamp, @balance, @balance_after_operation, @type)";

        using var connection = new NpgsqlConnection(_connectionString);
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("username", NpgsqlDbType.Text, operation.Username);
        command.Parameters.AddWithValue("amount", NpgsqlDbType.Numeric, operation.Amount);
        command.Parameters.AddWithValue(
            "timestamp",
            NpgsqlDbType.Timestamp,
            DateTime.SpecifyKind(operation.Timestamp, DateTimeKind.Local));
        command.Parameters.AddWithValue("balance", NpgsqlDbType.Numeric, operation.Balance);
        command.Parameters.AddWithValue(
            "balance_after_operation",
            NpgsqlDbType.Numeric,
            operation.BalanceAfterOperation);
        command.Parameters.AddWithValue("type", NpgsqlDbType.Text, operation.Type.ToString());

        connection.Open();

        command.ExecuteNonQuery();
    }
}