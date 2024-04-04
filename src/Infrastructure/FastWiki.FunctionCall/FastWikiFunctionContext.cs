using Microsoft.ClearScript.V8;

namespace FastWiki.FunctionCall;

public sealed class FastWikiFunctionContext : IDisposable
{
    public readonly V8ScriptEngine Engine = new();

    public FastWikiFunctionContext()
    {
        Engine.AddHostType("console", typeof(Console));
    }

    public void AddHostObject(string name, object obj)
    {
        Engine.AddHostObject(name, obj);
    }

    public void AddHostType(string name, Type type)
    {
        Engine.AddHostType(name, type);
    }

    public object FunctionCall(string script, string functionName, params object[] args)
    {
        Engine.Execute(script);

        object v = Engine.Invoke(functionName, args);

        return v;
    }

    public T FunctionCall<T>(string script, string functionName, params object[] args)
    {
        var result = FunctionCall(script, functionName, args);

        if (result is T t)
        {
            return t;
        }

        return default;
    }

    public async ValueTask<T> FunctionCallAsync<T>(string script, string functionName, params object[] args)
    {
        var result = await Task.Run(() => FunctionCall<T>(script, functionName, args));
        return result;
    }

    public void Dispose()
    {
        Engine.Dispose();
    }
}