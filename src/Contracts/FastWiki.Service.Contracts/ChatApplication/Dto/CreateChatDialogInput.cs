namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class CreateChatDialogInput
{
    public string Name { get; set; }

    public long WikiId { get; set; }

    public string Description { get; set; }
}