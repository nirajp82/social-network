using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Util
{
    public static class ServiceExtensions
    {
        public static void ConfigureUtilServices(this IServiceCollection services)
        {
            services.AddSingleton<ICryptoHelper, CryptoHelper>();
            services.AddSingleton<IMapperHelper, MapperHelper>();
            services.AddSingleton<AppConfigHelper>();
            services.AddSingleton<UtilFactory>();
        }
    }
}
