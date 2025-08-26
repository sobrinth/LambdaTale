using Xunit.Sdk;
using Xunit.v3;

namespace LambdaTale.Sdk;

/// <summary>
/// Represents a scenario.
/// </summary>
public interface IScenario : ITest
{
    /// <summary>
    /// Gets the display name of the scenario.
    /// </summary>
    new string TestDisplayName { get; }

    /// <summary>
    /// Gets the scenario outline this scenario belongs to.
    /// </summary>
    IXunitTestCase ScenarioOutline { get; }
}
