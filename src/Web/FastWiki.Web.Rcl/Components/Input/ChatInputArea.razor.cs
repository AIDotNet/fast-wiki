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

    [Parameter] public EventCallback<string> OnSubmit { get; set; }

    private void OnFocus(FocusEventArgs args)
    {
        Class = "input-affix-wrapper-focused";
    }

    private async Task OnKeyDown(KeyboardEventArgs args)
    {
        if (args is { Key: "Enter", ShiftKey: false, CtrlKey: false, AltKey: false, MetaKey: false })
        {
            // 获取value参数
           await Submit();
        }
    }

    private void OnBlur(FocusEventArgs obj)
    {
        Class = string.Empty;
    }

    private async Task Submit()
    {
        if (Value.IsNullOrEmpty())
        {
            return;
        }
        await OnSubmit.InvokeAsync(Value);
        Value = string.Empty;
    }
}
