using Xunit;

namespace LambdaTale.v3.Tests;

public class ScenarioTests
{
    [Fact]
    public void ShouldFail() => Assert.True(false);


    [Scenario]
    public void Scenario()
    {
        Assert.False(true);
    }
}
