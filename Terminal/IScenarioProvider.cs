using System.Diagnostics.CodeAnalysis;

namespace Terminal;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}