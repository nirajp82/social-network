using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Nucleus;

namespace SocialNetwork.API
{
    public static class ServiceConfiguration
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureNucleusServices(configuration);
            services.ConfigureSwaggerService();
        }
    }
}
