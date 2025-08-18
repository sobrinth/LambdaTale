using LambdaTale.Sdk;
using Xunit.Sdk;
using Xunit.v3;

namespace LambdaTale.Execution;

public class Scenario : LongLivedMarshalByRefObject, IScenario
{
    public Scenario(IXunitTestCase scenarioOutline, string displayName)
    {
        this.ScenarioOutline = scenarioOutline;
        this.DisplayName = displayName;
    }

    public IXunitTestCase ScenarioOutline { get; }

    public string DisplayName { get; }

    public ITestCase TestCase => this.ScenarioOutline;
}
