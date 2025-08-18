using System;
using System.Collections.Generic;
using LambdaTale.Sdk;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public class StepTest : IStep
{
    public StepTest(IScenario scenario, string displayName)
    {
        this.Scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
        this.TestDisplayName = displayName;
    }

    public IScenario Scenario { get; }

    public string TestDisplayName { get; }

    public ITestCase TestCase => this.Scenario.ScenarioOutline;

    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Traits { get; } = []; // TODO: what should this be

    public string UniqueID { get; } = "// TODO: Add a unique ID";
}
