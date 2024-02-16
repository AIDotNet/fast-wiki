using FastWiki.Service.Contracts.Wikis.Dto;
using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components.Forms;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class AddWiki
{
    private bool Visible { get; set; }
    private InputFile? inputFile;
    private ElementReference previewImageElem;
    private IBrowserFile _avatarBrowserFile;
    private CreateWikiInput input = new();

    [Parameter]
    public EventCallback OnSucceed { get; set; }

    private async Task ShowPreview(InputFileChangeEventArgs args)
    {
        _avatarBrowserFile = args.File;
        await JsHelperModule.PreviewImage(inputFile?.Element, previewImageElem);
    }

    private async Task OpenInputFile()
    {
        await JsHelperModule.ClickElement(inputFile?.Element);
    }

    public async Task OnSubmit()
    {
        if (string.IsNullOrEmpty(input.Name))
        {
            await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("知识库名称不能为空", AlertTypes.Warning));
            return;
        }

        if (string.IsNullOrEmpty(input.Model))
        {
            await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("知识库模型不能为空", AlertTypes.Warning));
            return;
        }

        var result = await StorageService.UploadFile(_avatarBrowserFile.OpenReadStream(), _avatarBrowserFile.Name);

        input.Icon = result.Path;

        await WikiService.CreateAsync(input);

        Visible = false;
        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("添加完成", AlertTypes.Success));

        await OnSucceed.InvokeAsync();
    }
}