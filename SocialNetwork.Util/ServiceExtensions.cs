using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.Util
{
    public static class ServiceExtensions
    {
        public static void ConfigureUtilServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoHelper, CryptoHelper>();
            services.AddScoped<IMapperHelper, MapperHelper>();
            services.AddSingleton<AppConfigHelper>();
            services.AddScoped<UtilFactory>();
        }
    }
}
