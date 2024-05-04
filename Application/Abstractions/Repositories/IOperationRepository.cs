using Models.Operations;

namespace Abstractions.Repositories;

public interface IOperationRepository
{
    IEnumerable<Operation> GetOperationHistoryByUsername(string username);
    void AddOperation(Operation operation);
}