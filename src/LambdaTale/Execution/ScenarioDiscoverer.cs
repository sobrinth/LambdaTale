using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit.Internal;
using Xunit.Sdk;
using Xunit.v3;

namespace LambdaTale.Execution;

public class ScenarioDiscoverer : TheoryDiscoverer
{
    public async override ValueTask<IReadOnlyCollection<IXunitTestCase>> Discover(
        ITestFrameworkDiscoveryOptions discoveryOptions,
        IXunitTestMethod testMethod,
        IFactAttribute factAttribute)
    {
        Guard.ArgumentNotNull(discoveryOptions);
        Guard.ArgumentNotNull(testMethod);
        Guard.ArgumentNotNull(factAttribute);

        if (factAttribute is not IScenarioAttribute)
        {
            throw new ArgumentException("ScenarioDiscoverer.Discover must be passed an attribute that implements IScenarioAttribute", nameof(factAttribute));
        }

        if (factAttribute.Skip is not null && factAttribute.SkipUnless is null && factAttribute.SkipWhen is null)
        {
            // return await base.CreateTestCasesForTheory(discoveryOptions, testMethod, factAttribute);
        }



        discoveryOptions = discoveryOptions ?? throw new ArgumentNullException(nameof(discoveryOptions));

        // return new ScenarioOutlineTestCase(
        //     this.DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod);
        throw new NotImplementedException();
    }
}
