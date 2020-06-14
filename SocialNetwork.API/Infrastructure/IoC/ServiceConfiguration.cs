using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Infrastructure;
using SocialNetwork.Nucleus;
using SocialNetwork.Util;

namespace SocialNetwork.API
{
    public static class ServiceConfiguration
    {       
        #region Public Methods
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureUtilServices();
            services.ConfigureInfrastructureServices(configuration);
            services.ConfigureNucleusServices(configuration);
            services.ConfigureSwaggerService();

            services.AddScoped<ValidateActivityExistsFilter>();
        }

        public static void AddCorsSupport(this IServiceCollection services,string policyName)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            AppConfigHelper appConfigHelper = serviceProvider.GetService<AppConfigHelper>();

            services.AddCors(options =>
            {
                options.AddPolicy(policyName, corsOptions =>
                {
                    corsOptions
                        .WithOrigins(appConfigHelper.GetValue<string>("Cors:AllowedHost"))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
        #endregion


        #region Private Methods
        #endregion
    }
}
