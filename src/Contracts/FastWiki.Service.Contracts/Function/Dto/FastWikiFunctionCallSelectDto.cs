namespace FastWiki.Service.Contracts.Function.Dto;

public class FastWikiFunctionCallSelectDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    ///     进入function call的主函数
    /// </summary>
    public string Main { get; set; }
}