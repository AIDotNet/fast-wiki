namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ShareChat
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string Id { get; set; }


}
