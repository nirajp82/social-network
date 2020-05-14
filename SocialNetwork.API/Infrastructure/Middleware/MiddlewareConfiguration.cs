using Microsoft.AspNetCore.Builder;

namespace SocialNetwork.API
{
    public static class MiddlewareConfiguration
    {
        public static void ConfigureCommonMiddleware(this IApplicationBuilder appBuilder)
        {
            appBuilder.ConfigureSwaggerMiddleware();
        }
    }
}
