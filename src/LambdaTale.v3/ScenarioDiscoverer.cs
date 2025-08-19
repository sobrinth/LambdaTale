using Xunit.Internal;
using Xunit.Sdk;
using Xunit.v3;

namespace LambdaTale.v3;

public class ScenarioDiscoverer : IXunitTestCaseDiscoverer
{
    public async ValueTask<IReadOnlyCollection<IXunitTestCase>> Discover(
        ITestFrameworkDiscoveryOptions discoveryOptions,
        IXunitTestMethod testMethod,
        IFactAttribute factAttribute)
    {
        Guard.ArgumentNotNull(discoveryOptions);
        Guard.ArgumentNotNull(testMethod);
        Guard.ArgumentNotNull(factAttribute);

        if (factAttribute is not ScenarioAttribute)
        {
            throw new ArgumentException("ScenarioDiscoverer.Discover must be passed an ScenarioAttribute",
                nameof(factAttribute));
        }

        if (factAttribute.Skip is not null && factAttribute.SkipUnless is null && factAttribute.SkipWhen is null)
        {
            // return await base.CreateTestCasesForTheory(discoveryOptions, testMethod, factAttribute);
        }

        var details =
            TestIntrospectionHelper.GetTestCaseDetails(discoveryOptions, testMethod, factAttribute, null, null, null);

        IXunitTestCase testCase = new ScenarioTestCase(
            details.ResolvedTestMethod,
            details.TestCaseDisplayName,
            details.UniqueID,
            details.Explicit,
            details.SkipExceptions,
            details.SkipReason,
            details.SkipType,
            details.SkipUnless,
            details.SkipWhen,
            testMethod.Traits.ToReadWrite(StringComparer.OrdinalIgnoreCase),
            details.SourceFilePath,
            details.SourceLineNumber,
            details.Timeout);

        return await new ValueTask<IReadOnlyCollection<IXunitTestCase>>([testCase]);
    }
}
