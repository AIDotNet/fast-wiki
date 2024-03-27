namespace FastWiki.Service.Contracts.Model.Dto;

public class FeiShuChatResult : FeiShuChatResultBase
{
    public object data { get; set; }
}
public class FeiShuChatResult<T> : FeiShuChatResultBase
{
    public T data { get; set; }
}
public abstract class FeiShuChatResultBase
{
    public int code { get; set; }
    public string msg { get; set; }
}
