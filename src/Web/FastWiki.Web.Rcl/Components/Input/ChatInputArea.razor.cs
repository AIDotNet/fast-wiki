using Microsoft.AspNetCore.Components.Web;

namespace FastWiki.Web.Rcl.Components.Input;

public partial class ChatInputArea
{
    [Parameter]
    public string Value { get; set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter] public RenderFragment FuntionContent { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter] public StringNumber Width { get; set; } = "100%";

    [Parameter] public StringNumber Height { get; set; }
    [Parameter] public string Placeholder { get; set; }

    private void OnFocus(FocusEventArgs args)
    {
        Class = "input-affix-wrapper-focused";
    }


    private void OnBlur(FocusEventArgs obj)
    {
        Class = string.Empty;
    }
}
