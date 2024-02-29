namespace FastWiki.Web.Rcl.Global;

public class CookieStorage(IJSRuntime jsRuntime)
{
    public async Task<string> GetAsync(string key)
    {
        return await jsRuntime.InvokeAsync<string>(JsInteropConstants.GetCookie, key);
    }

    public async void SetAsync<T>(string key, T? value)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync(JsInteropConstants.SetCookie, key, value?.ToString());
        }
        catch
        {
            // ignored
        }
    }
}
