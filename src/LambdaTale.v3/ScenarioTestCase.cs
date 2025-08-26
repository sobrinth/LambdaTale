using System.Reflection;
using Xunit.Internal;
using Xunit.Sdk;
using Xunit.v3;

namespace LambdaTale.v3;

public class ScenarioTestCase : XunitTestCase
{
    public ScenarioTestCase() { }

    public ScenarioTestCase(
        IXunitTestMethod testMethod,
        string testCaseDisplayName,
        string uniqueId,
        bool @explicit,
        Type[]? skipExceptions = null,
        string? skipReason = null,
        Type? skipType = null,
        string? skipUnless = null,
        string? skipWhen = null,
        Dictionary<string, HashSet<string>>? traits = null,
        string? sourceFilePath = null,
        int? sourceLineNumber = null,
        int? timeout = null) :
        base(
            testMethod,
            testCaseDisplayName,
            uniqueId,
            @explicit,
            skipExceptions,
            skipReason,
            skipType,
            skipUnless,
            skipWhen,
            traits,
            testMethodArguments: null,
            sourceFilePath,
            sourceLineNumber,
            timeout
        )
    {
    }

    public override ValueTask<IReadOnlyCollection<IXunitTest>> CreateTests()
    {
        /*
         * We currently have a problem when trying to create testmethods based on all the steps...
         * One really "brute-forcy" way is to "translate" the steps to private methods with a roslyn generator and tag
         * them with a "special" attribute?
         *
         */

        var result = new List<IXunitTest>();
        var testindex = 0;
        using var ctx = Scenario.Acquire();

        var tc = Activator.CreateInstance(this.TestMethod.TestClass.Class);
        this.TestMethod.Method.Invoke(tc, this.TestMethodArguments);
        var res = Scenario.TestDefinitions;


        foreach (var stepDefinition in res)
        {
            var tm = new XunitTestMethod(new ScenarioTestClass(this.TestCollection, stepDefinition.Lambda.Method), stepDefinition.Lambda.Method, [], null);

            var x = new XunitTest(
                this,
                tm, // TODO repl
                this.Explicit,
                this.SkipReason,
                this.SkipType,
                this.SkipUnless,
                this.SkipWhen,
                stepDefinition.Tale,
                testIndex: testindex++,
                this.Traits.ToReadOnly(),
                this.Timeout,
                []);
            result.Add(x);
        }

        return new(result);
    }
}

public class ScenarioTestClass : XunitTestClass, IXunitSerializable
{
    private object? target;
    public ScenarioTestClass(IXunitTestCollection testCollection, MethodInfo lambdaInfo, string? uniqueID = null) : base(lambdaInfo.DeclaringType!, testCollection, uniqueID)
    {
        this.target = Activator.CreateInstance(lambdaInfo.DeclaringType!);
    }

    public new Type Class => this.target!.GetType();
}
