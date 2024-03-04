using Microsoft.JSInterop;

namespace FastWiki.Infrastructure.Rcl.Command.JsInterop;

public sealed class JsHelperJsInterop : JSModule
{
    /// <inheritdoc />
    public JsHelperJsInterop(IJSRuntime js) : base(js, PrefixPath + "js-helper.js")
    {
    }

    public async ValueTask OpenUrl(string url, string type = "_blank")
    {
        await InvokeVoidAsync("openUrl", url, type);
    }
    
    public async ValueTask ScrollToBottom(string elementId)
    {
        await InvokeVoidAsync("scrollToBottom", elementId);
    }
}