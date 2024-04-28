using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastWiki.Service.Dto;
using Masa.Contrib.Authentication.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FastWiki.Service.Infrastructure.Helper;


/// <summary>
/// JWT帮助类
/// </summary>
public class JWTHelper
{
    /// <summary>
    /// ����token
    /// </summary>
    /// <param name="claimsIdentity"></param>
    /// <returns></returns>
    public static string GeneratorAccessToken(ClaimsIdentity claimsIdentity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(JwtOptions.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// 创建token
    /// </summary>
    /// <param name="claimsIdentity"></param>
    /// <returns></returns>
    public static string GeneratorAccessToken(UserDto user)
    {
        var claimsIdentity = GetClaimsIdentity(user);

        return GeneratorAccessToken(claimsIdentity);
    }


    public static ClaimsIdentity GetClaimsIdentity(UserDto user)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new(ClaimType.DEFAULT_USER_NAME, user.Account),
            new(ClaimType.DEFAULT_USER_ID, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
            new("IsDisable", user.IsDisable.ToString()),
        });
    }

    public static UserDto GetCurrentUser(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal.FindFirstValue(ClaimType.DEFAULT_USER_ID) is null)
            return null;
        var user = new UserDto
        {
            Account = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimType.DEFAULT_USER_NAME)?.Value,
            Id = Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimType.DEFAULT_USER_ID)?.Value!),
            IsDisable = bool.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "IsDisable")?.Value!),
        };

        return user;
    }

    public static UserDto? GetCurrentUser(string token)
    {
        if (token == null)
        {
            return null;
        }

        try
        {
            // 解析token
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var user = new UserDto()
            {
                Account = jwt.Claims.FirstOrDefault(x => x.Type == ClaimType.DEFAULT_USER_NAME)?.Value,
                Id = Guid.Parse(jwt.Claims.FirstOrDefault(x => x.Type == ClaimType.DEFAULT_USER_ID)?.Value!),
                IsDisable = bool.Parse(jwt.Claims.FirstOrDefault(x => x.Type == "IsDisable")?.Value!),
            };
            return user;
        }
        catch (Exception)
        {
            return null;
        }
    }
}