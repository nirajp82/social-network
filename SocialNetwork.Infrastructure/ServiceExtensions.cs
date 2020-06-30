using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Nucleus;
using System.Threading.Tasks;

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
            services.AddSingleton<IPhotoAccessor, PhotoAccessor>();
            services.AddSingleton<InfrastructureConfigSettings>();
            services.ConfigureSwaggerService();

            //Action Filters
            services.AddScoped<ValidateActivityExistsFilter>();
            services.AddScoped<ValidateAttendanceFilter>();
            services.AddScoped<ValidateUnAttendanceFilter>();

            //Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = Helper.GenerateSecurityKey(configuration),
                        //TODO: Customize this options.
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };

                    opt.Events = new JwtBearerEvents
                    {
                       OnMessageReceived = (context) =>
                       {
                           //Set Access Token for Chat Request.
                           var accessToken = context.Request.Query[InfrastrctureConstants.ACCESS_TOKEN];
                           var path = context.HttpContext.Request.Path;
                           if (!string.IsNullOrWhiteSpace(accessToken) && path.StartsWithSegments(InfrastrctureConstants.CHAT_HUB))
                           {
                               context.Token = accessToken;
                           }
                           return Task.CompletedTask;
                       }
                    };
                });

            //Host Authorization
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(InfrastrctureConstants.ACTIVITY_HOST_POLICY_NAME, policy =>
                {
                    policy.Requirements.Add(new IsHostRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
        }
        #endregion
    }
}
