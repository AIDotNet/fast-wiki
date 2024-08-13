namespace FastWiki.Service.Contracts;

public static class Constant
{
    public const string ApiGatewayHttpClient = nameof(ApiGatewayHttpClient);

    public const string Token = nameof(Token);

    /// <summary>
    ///     ∂‘ª∞∑÷œÌ
    /// </summary>
    public const string ChatShare = nameof(ChatShare);

    public class Role
    {
        public const string Guest = nameof(RoleType.Guest);

        public const string User = nameof(RoleType.User);

        public const string Admin = nameof(RoleType.Admin);
    }
}