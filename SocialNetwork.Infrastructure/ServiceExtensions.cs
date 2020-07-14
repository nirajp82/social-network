using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Nucleus;

namespace SocialNetwork.Infrastructure
{
    public static class ServiceExtensions
    {
        #region Extension Method
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Services Registration
            RegisterServices(services);

            RegisterActionFilters(services);

            InitAuthSettings(services, configuration);
        }
        #endregion


        #region Private Methods       
        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ConfigSettings>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IFacebookAccessor, FacebookAccessor>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.ConfigureSwaggerService();
        }

        private static void RegisterActionFilters(IServiceCollection services)
        {
            //Action Filters
            services.AddScoped<ValidateExpiredTokenFilter>();
            services.AddScoped<ValidateActivityExistsFilter>();
            services.AddScoped<ValidateAttendanceFilter>();
            services.AddScoped<ValidateUnAttendanceFilter>();
        }

        private static void InitAuthSettings(IServiceCollection services, IConfiguration configuration)
        {
            //Authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = JWTTokenHelper.InitTokenValidationParameters(configuration, true);
                opt.Events = JWTTokenHelper.InitJwtBearerEvents();
            });

            //Host Authorization
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Constants.ACTIVITY_HOST_POLICY_NAME, policy =>
                {
                    policy.Requirements.Add(new IsHostRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
        }
        #endregion
    }
}
