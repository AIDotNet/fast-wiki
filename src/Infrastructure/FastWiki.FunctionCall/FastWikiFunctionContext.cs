using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

namespace FastWiki.FunctionCall;

public sealed class FastWikiFunctionContext : IDisposable
{
    public readonly V8ScriptEngine Engine = new();

    public FastWikiFunctionContext()
    {
        Engine.AddHostType("Console", typeof(Console));
        Engine.AddHostType("HttpClientHelper", typeof(HttpClientHelper));
    }

    public void Dispose()
    {
        Engine.Dispose();
    }

    public async Task AddHostObject(string name, object obj)
    {
        Engine.AddHostObject(name, obj);
    }

    public void AddHostType(string name, Type type)
    {
        Engine.AddHostType(name, type);
    }

    public async Task<object> FunctionCall(string script, string functionName, params object[] args)
    {
        Engine.Execute(script);
        dynamic resultOrPromise = Engine.Invoke(functionName, args);

        var t = resultOrPromise?.GetType();

        // 判断v是否空   
        if (t == Undefined.Value.GetType()) return null;

        bool isPromise = Engine.Script.Object.prototype.toString.call(resultOrPromise) == "[object Promise]";
        if (isPromise)
        {
            // 获取返回的结果
            var tcs = new TaskCompletionSource<object>();
            resultOrPromise.then(
                new Action<object>(value =>
                {
                    // 如果value返回的是Task 或Task<T>类型
                    value = value.GetType().GetProperty("Result")?.GetValue(value) ?? value;

                    tcs.SetResult(value);
                })
            );

            return tcs.Task.GetAwaiter().GetResult();
        }

        return resultOrPromise;
    }

    public async Task<T> FunctionCall<T>(string script, string functionName, params object[] args)
    {
        var result = await FunctionCall(script, functionName, args);

        if (result is T t) return t;

        return default;
    }

    public async ValueTask<T> FunctionCallAsync<T>(string script, string functionName, params object[] args)
    {
        var result = await Task.Run(() => FunctionCall<T>(script, functionName, args));
        return result;
    }
}