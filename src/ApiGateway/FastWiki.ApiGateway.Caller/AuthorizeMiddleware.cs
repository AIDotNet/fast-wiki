using System.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection;

public class AuthorizeMiddleware(IUserService userService) : ICallerMiddleware
{
    public async Task HandleAsync(MasaHttpContext masaHttpContext, CallerHandlerDelegate next,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (masaHttpContext.RequestMessage.Headers.Authorization == null)
        {
            var token = await userService.GetTokenAsync();
            if (!token.IsNullOrWhiteSpace())
            {
                masaHttpContext.RequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        await next();
    }
}