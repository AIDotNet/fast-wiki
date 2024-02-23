namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class AddChatApplication
{
    private bool Visible;

    [Parameter]
    public EventCallback OnSucceed { get; set; }

    private CreateChatApplicationInput input = new();

    private async Task OnSubmit()
    {
        if (string.IsNullOrEmpty(input.Name))
        {
            await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("应用名称不能为空", AlertTypes.Warning));
            return;
        }

        await ChatApplicationService.CreateAsync(input);

        Visible = false;

        await OnSucceed.InvokeAsync();
    }
}
