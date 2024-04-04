using FastWiki.FunctionCall;

namespace Test.FastWiki.FunctionCall;

public class FunctionContext
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestConsole()
    {
        using var function = new FastWikiFunctionContext();

        var result = function.FunctionCall("function print(x) { console.WriteLine(x); return x; }", "print",
            "Hello World!");
    }

    [Test]
    public void TestAdd()
    {
        using var function = new FastWikiFunctionContext();

        var result = function.FunctionCall<int>("function add(x) { x=x +1;return x; }", "add", 2);
        Assert.AreEqual(3, result);
    }
}