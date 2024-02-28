using Microsoft.JSInterop;

namespace FastWiki.Infrastructure.Rcl.Command.JsInterop;


/// <summary>
/// 加载任何JavaScript (ES6)模块并调用其导出项的Helper
/// </summary>
public abstract class JSModule : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    /// <summary>
    /// 文件地址前缀
    /// </summary>
    protected const string PrefixPath = "/_content/FastWiki.Infrastructure.Rcl.Command/js/";

    protected JSModule(IJSRuntime js, string moduleUrl)
        => _moduleTask = new Lazy<Task<IJSObjectReference>>(() => js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask());

    protected async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;

        try
        {
            await module.InvokeVoidAsync(identifier, args).ConfigureAwait(false);
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }

    protected async ValueTask<T?> InvokeAsync<T>(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;

        try
        {
            return await module.InvokeAsync<T?>(identifier, args).ConfigureAwait(false);
        }
        catch (JSDisconnectedException)
        {
            return default(T?);
        }
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;

            try
            {
                await module.DisposeAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}