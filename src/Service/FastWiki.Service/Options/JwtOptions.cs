namespace FastWiki.Service;

public class JwtOptions
{
    public const string Name = "Jwt";
    
    /// <summary>
    /// ÃÜÔ¿
    /// </summary>
    public static string Secret { get; set; }

    /// <summary>
    /// ÓÐÐ§ÆÚ
    /// </summary>
    public static int EffectiveHours { get; set; }
}