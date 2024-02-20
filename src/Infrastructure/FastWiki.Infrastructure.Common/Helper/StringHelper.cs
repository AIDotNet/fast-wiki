namespace FastWiki.Infrastructure.Common.Helper;

public static class StringHelper
{
    /// <summary>
    /// 将byte转换字符串
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FormatBytes(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
        int suffixIndex = 0;
        double size = bytes;

        while (size >= 1024 && suffixIndex < suffixes.Length - 1)
        {
            size /= 1024;
            suffixIndex++;
        }

        return $"{size:0.##} {suffixes[suffixIndex]}";
    }
}