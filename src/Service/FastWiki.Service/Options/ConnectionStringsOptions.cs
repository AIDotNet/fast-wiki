namespace FastWiki.Service;

public class ConnectionStringsOptions
{
    public const string Name = "ConnectionStrings";

    public static string DefaultConnection { get; set; }

    public static string TableNamePrefix { get; set; }
    
    public static string DefaultType { get; set; }
    
    public static string WikiType { get; set; }
    
    public static string WikiConnection { get; set; }
}