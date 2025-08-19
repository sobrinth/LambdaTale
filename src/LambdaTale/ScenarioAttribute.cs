using System;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.v3;

namespace LambdaTale;

/// <summary>
/// Applied to a method to indicate the definition of a scenario.
/// A scenario can also be fed examples from a data source, mapping to parameters on the scenario method.
/// If the data source contains multiple rows,
/// then the scenario method is executed multiple times (once with each data row).
/// Examples can be fed to the scenario by applying one or more instances of <see cref="ExampleAttribute"/>
/// or any other attribute inheriting from <see cref="DataAttribute"/>.
/// E.g. <see cref="Xunit.InlineDataAttribute"/> or
/// <see cref="Xunit.MemberDataAttribute"/>.
/// </summary>
[XunitTestCaseDiscoverer(typeof(Execution.ScenarioDiscoverer))]
[AttributeUsage(AttributeTargets.Method)]
public class ScenarioAttribute(
    [CallerFilePath] string? sourceFilePath = null,
    [CallerLineNumber] int sourceLineNumber = -1) :
    FactAttribute(sourceFilePath, sourceLineNumber), IScenarioAttribute
{
    /// <inheritdoc/>
    public bool DisableDiscoveryEnumeration { get; }

    /// <inheritdoc/>
    public bool SkipTestWithoutData { get; }
}
