using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Nucleus;
using SocialNetwork.Util;

namespace SocialNetwork.API
{
    public static class ServiceConfiguration
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureUtilServices();
            services.ConfigureNucleusServices(configuration);
            services.ConfigureSwaggerService();
        }
    }
}
