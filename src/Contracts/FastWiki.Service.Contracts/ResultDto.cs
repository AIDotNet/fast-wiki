namespace FastWiki.Service.Contracts;

public class ResultDto
{
    public string Code { get; set; }

    public string? Message { get; set; }

    public object Data { get; set; }

    public static ResultDto CreateSucceed(object data)
    {
        return new ResultDto()
        {
            Code = "200",
            Data = data
        };
    }

    public static ResultDto CreateError(string message, string code)
    {
        return new ResultDto()
        {
            Message = message,
            Code = code
        };
    }
}

public class ResultDto<TResult>
{
    public string Code { get; set; }

    public string? Message { get; set; }

    public object Data { get; set; }

    public static ResultDto<TResult> CreateSucceed(object data)
    {
        return new ResultDto<TResult>()
        {
            Code = "200",
            Data = data
        };
    }
}