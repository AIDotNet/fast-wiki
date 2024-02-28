namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class CreateChatShareInput
{
    public string Name { get; set; }
    public string ChatApplicationId { get; set; }
    public DateTime Expires { get; set; }
    public long AvailableToken { get; set; }
    public int AvailableQuantity { get; set; }
}