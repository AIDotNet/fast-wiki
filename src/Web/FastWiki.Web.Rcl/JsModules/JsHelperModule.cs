namespace FastWiki.Web.Rcl.JsModules;

public sealed class JsHelperModule(IJSRuntime js) : JSModule(js, "/_content/FastWiki.Web.Rcl/js/js-helper.js")
{
    public async ValueTask PreviewImage(ElementReference? inputFile, ElementReference img)
    {
        await InvokeVoidAsync("previewImage", inputFile, img);
    }

    public async ValueTask Click(string id)
    {
        await InvokeVoidAsync("click", id);
    }

    public async ValueTask ClickElement(ElementReference? element)
    {
        await InvokeVoidAsync("clickElement", element);
    }
}