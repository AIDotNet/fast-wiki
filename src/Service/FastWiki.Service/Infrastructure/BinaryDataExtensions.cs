using System.Security.Cryptography;

namespace FastWiki.Service.Infrastructure;

public static class BinaryDataExtensions
{
    public static string CalculateSHA256(this BinaryData data)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(data.ToArray());
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}