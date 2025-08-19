using System.Runtime.CompilerServices;
using Xunit;
using Xunit.v3;

namespace LambdaTale.v3;

[XunitTestCaseDiscoverer(typeof(ScenarioDiscoverer))]
[AttributeUsage(AttributeTargets.Method)]
public class ScenarioAttribute(
    [CallerFilePath] string? sourceFilePath = null,
    [CallerLineNumber] int sourceLineNumber = -1) :
    FactAttribute(sourceFilePath, sourceLineNumber)
{
    public bool DisableDiscoveryEnumeration { get; }

    public bool SkipTestWithoutData { get; }
}
