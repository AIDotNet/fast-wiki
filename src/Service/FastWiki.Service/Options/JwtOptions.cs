namespace FastWiki.Service;

public class JwtOptions
{
    public const string Name = "Jwt";
    
    /// <summary>
    /// 秘钥
    /// </summary>
    public static string Secret { get; set; }

    /// <summary>
    /// 有效时间（小时）
    /// </summary>
    public static int EffectiveHours { get; set; }
}