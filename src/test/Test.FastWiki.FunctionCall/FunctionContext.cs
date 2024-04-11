using FastWiki.FunctionCall;

namespace Test.FastWiki.FunctionCall;

public class FunctionContext
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TaskGetCity()
    {
        using var function = new FastWikiFunctionContext();

        var result = await function.FunctionCall(
"""

async function GetCity(city) {
	const str = `https://api.seniverse.com/v3/weather/now.json?key=SqskMHsGbF6Ctge2D&location=${city}&language=zh-Hans&unit=c`;
	const data = await HttpClientHelper.GetAsync(str)
	return data;
} 
            
""", "GetCity",
            "深圳");
    }

    [Test]
    public async Task TestAdd()
    {
        using var function = new FastWikiFunctionContext();

        var result = await function.FunctionCall("function add(x) { x=x +1;return x; }", "add", 2);
        Assert.AreEqual(3, result);
    }
}