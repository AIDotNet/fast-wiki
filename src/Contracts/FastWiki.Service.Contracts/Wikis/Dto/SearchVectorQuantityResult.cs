namespace FastWiki.Service.Contracts.Wikis.Dto;

public class SearchVectorQuantityResult
{
    public double ElapsedTime { get; set; }

    public List<SearchVectorQuantityDto> Result { get; set; } = new();
}