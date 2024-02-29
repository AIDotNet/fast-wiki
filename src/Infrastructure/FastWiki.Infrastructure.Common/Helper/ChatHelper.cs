namespace FastWiki.Infrastructure.Common.Helper;

public class ChatHelper
{
    public static List<(string, string)> GetChatModel()
    {
        var models = new List<(string, string)>
        {
            new("gpt-3.5-turbo", "gpt-3.5-turbo"),
            new("gpt-4-0125-preview", "gpt-4-0125-preview"),
            new("gpt-4-1106-preview", "gpt-4-1106-preview"),
            new("gpt-4-1106-vision-preview", "gpt-4-1106-vision-preview"),
            new("gpt-4", "gpt-4"),
            new("gpt-4-32k", "gpt-4-32k"),
            new("gpt-3.5-turbo-0125", "gpt-3.5-turbo-0125")
        };

        return models;
    }
}