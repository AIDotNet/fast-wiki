namespace FastWiki.Service.Contracts.ChatApplication.Dto;

public class CreateChatShareInput
{
    public string Name { get; set; }

    public string ChatApplicationId { get; set; }

    public DateTime Expires { get; set; } = DateTime.Now.AddDays(7);

    public long AvailableToken { get; set; } = -1;

    public int AvailableQuantity { get; set; } = -1;
}