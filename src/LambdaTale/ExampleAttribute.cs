using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Xunit.v3;

namespace LambdaTale;

/// <summary>
/// Provides example values for a scenario passed as arguments to the scenario method.
/// This attribute is designed as a synonym of <see cref="Xunit.InlineDataAttribute"/>,
/// which is the most commonly used data attribute, but you can also use any type of attribute derived from
/// <see cref="DataAttribute"/> to provide a data source for a scenario.
/// E.g. <see cref="Xunit.InlineDataAttribute"/> or
/// <see cref="Xunit.MemberDataAttribute"/>.
/// </summary>
/// /// <param name="data">The data values to pass to the scenario.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ExampleAttribute(params object?[]? data) : DataAttribute
{
    /// <summary>
    /// Gets the data to be passed to the test.
    /// </summary>
    // If the user passes null to the constructor, we assume what they meant was a
    // single null value to be passed to the test.
    public object?[] Data { get; } = data ?? [null];

    /// <inheritdoc/>
    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        var traits = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        TestIntrospectionHelper.MergeTraitsInto(traits, this.Traits);

        return new([
            new TheoryDataRow(this.Data)
            {
                Explicit = this.ExplicitAsNullable,
                Label = this.Label,
                Skip = this.Skip,
                TestDisplayName = this.TestDisplayName,
                Timeout = this.TimeoutAsNullable,
                Traits = traits
            }
        ]);
    }

    /// <inheritdoc/>
    public override bool SupportsDiscoveryEnumeration() => true;
}
