using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Util
{
    public static class ServiceExtensions
    {
        public static void ConfigureUtilServices(this IServiceCollection services)
        {
            services.AddSingleton<AppConfigHelper>();
            services.AddScoped<ICryptoHelper, CryptoHelper>();
            services.AddScoped<IMapperHelper, MapperHelper>();
            services.AddScoped<UtilFactory>();
        }
    }
}
