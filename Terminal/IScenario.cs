namespace Terminal;

public interface IScenario
{
    string Name { get; }

    void Run();
}