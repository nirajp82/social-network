using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Infrastructure
{
    public static class ServiceExtensions
    {
        #region Member
        #endregion


        #region Extension Method
        public static void ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }
        #endregion
    }
}
