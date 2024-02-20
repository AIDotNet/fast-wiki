using Microsoft.AspNetCore.Components.Forms;

namespace FastWiki.Web.Rcl.Components.Upload;

public partial class DropUpload
{
    [Parameter]
    public bool Directory { get; set; }

    [Parameter]
    public bool Multiple { get; set; }

    [Parameter]
    public string? Accept { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public EventCallback<InputFileChangeEventArgs> OnUploadChanged { get; set; }

    private string Id;

    protected override void OnInitialized()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}
