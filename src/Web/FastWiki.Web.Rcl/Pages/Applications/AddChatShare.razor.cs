using Microsoft.AspNetCore.Components.Web;

namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class AddChatShare
{
    private bool Visible { get; set; }

    private CreateChatShareInput input = new();

    [Parameter]
    public EventCallback<CreateChatShareInput> OnSubmit { get; set; }

    private async Task Submit(MouseEventArgs arg)
    {
        await OnSubmit.InvokeAsync(input);

        input = new CreateChatShareInput();
        Visible = false;
    }
}
