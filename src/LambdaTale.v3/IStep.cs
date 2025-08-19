using Xunit.v3;

namespace LambdaTale.v3;

public interface IStep : IXunitTest
{
    Action Body { get; set; }
}
