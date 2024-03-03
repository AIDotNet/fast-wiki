namespace FastWiki.Service;

public class JwtOptions
{
    public const string Name = "Jwt";
    
    /// <summary>
    /// ???
    /// </summary>
    public static string Secret { get; set; }

    /// <summary>
    /// ??§¹???§³???
    /// </summary>
    public static int EffectiveHours { get; set; }
}