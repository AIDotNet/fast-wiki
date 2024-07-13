namespace FastWiki.Service.Options;

public class WebOptions
{
    public static void Init(IConfiguration configuration)
    {
        DEFAULT_MODEL = Environment.GetEnvironmentVariable("DEFAULT_MODEL") ?? configuration["Thor:DefaultModel"];
        DEFAULT_AVATAR = Environment.GetEnvironmentVariable("DEFAULT_AVATAR") ?? configuration["Thor:DefaultAvatar"];
        DEFAULT_USER_AVATAR = Environment.GetEnvironmentVariable("DEFAULT_USER_AVATAR") ??
                              configuration["Thor:DefaultUserAvatar"];
        DEFAULT_INBOX_AVATAR = Environment.GetEnvironmentVariable("DEFAULT_INBOX_AVATAR") ??
                               configuration["Thor:DefaultInboxAvatar"];
    }

    public static string DEFAULT_MODEL { get; set; } = "gpt-3.5-turbo";

    public static string DEFAULT_AVATAR { get; set; } = "🤖";

    public static string DEFAULT_USER_AVATAR { get; set; } = "😀";

    public static string DEFAULT_INBOX_AVATAR { get; set; } = "🤯";
}