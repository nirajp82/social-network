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
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddSingleton<IFacebookAccessor, FacebookAccessor>();
            services.AddSingleton<IPhotoAccessor, PhotoAccessor>();
            services.AddSingleton<ConfigSettings>();
            services.ConfigureSwaggerService();

            //Action Filters
            services.AddScoped<ValidateActivityExistsFilter>();
            services.AddScoped<ValidateAttendanceFilter>();
            services.AddScoped<ValidateUnAttendanceFilter>();

            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => JWTTokenHelper.InitJwtBearerOptions(opt, configuration));                

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
