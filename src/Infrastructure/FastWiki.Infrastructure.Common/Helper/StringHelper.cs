namespace FastWiki.Infrastructure.Common.Helper;

/// <summary>
/// 字符串帮助类
/// </summary>
public static class StringHelper
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    /// <summary>
    /// 将byte转换字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FormatBytes(long bytes)
    {
        string[] suffixes = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
        int suffixIndex = 0;
        double size = bytes;

        while (size >= 1024 && suffixIndex < suffixes.Length - 1)
        {
            size /= 1024;
            suffixIndex++;
        }

        return $"{size:0.##} {suffixes[suffixIndex]}";
    }

    /// <summary>
    /// 随机字符串长度的字符串
    /// </summary>
    public static string GenerateRandomString(int length)
    {
        return new string(Enumerable.Repeat(Chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}