using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = Helper.SecurityKey,
                        //TODO: Customize this options.
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
        }
        #endregion
    }
}
