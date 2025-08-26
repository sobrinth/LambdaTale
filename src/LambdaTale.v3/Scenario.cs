namespace LambdaTale.v3;

public class Scenario
{
    private static AsyncLocal<List<ScenarioStepDefinition>?> tests = new();

    public static IDisposable Acquire()
    {
        tests.Value = [];
        return ScenarioContext.Instance;
    }

    public static void Add(ScenarioStepDefinition testDefinition)
    {
        var context = tests.Value ?? MissingContext();
        context.Add(testDefinition);
    }

    public static IEnumerable<ScenarioStepDefinition> TestDefinitions =>
        tests.Value ?? MissingContext();

    private static List<ScenarioStepDefinition> MissingContext()
    {
        throw new InvalidOperationException("Missing " + nameof(ScenarioContext));
    }

    private sealed class ScenarioContext : IDisposable
    {
        public static readonly IDisposable Instance = new ScenarioContext();

        public void Dispose()
        {
            tests.Value = null;
        }
    }
}
