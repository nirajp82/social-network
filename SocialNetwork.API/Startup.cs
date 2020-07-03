using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Util;
using SocialNetwork.WebUtil;
using System.Net;
using SocialNetwork.Infrastructure;
using SocialNetwork.Nucleus;

namespace SocialNetwork.API
{
    public class Startup
    {
        #region Members
        private readonly IConfiguration _configuration;
        private const string _CORS_POLICY_NAME = "SocialNetworkCorsPolicy";
        private const string _CORS_ALLOWED_HOST_KEY = "Cors:AllowedHost";
        #endregion


        #region Constructor
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion


        // This method gets called by the runtime. Use this method to add services to the container.
        #region Public Methods
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppServices(services);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(TaskCanceledExceptionFilter));
                ConfigureAuthorizationPolicy(options);
            })
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Create>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseCors(_CORS_POLICY_NAME);

            app.ConfigureSwaggerMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ActivityChatHub>(Constants.ACTIVITY_CHAT_HUB);
            });
        }
        #endregion


        #region Private Methods
        private void ConfigureAppServices(IServiceCollection services)
        {
            services.ConfigureUtilServices();
            services.ConfigureInfrastructureServices(_configuration);
            services.ConfigureNucleusServices(_configuration);
            AddCorsSupport(services);
            services.AddSignalR();
        }

        private void AddCorsSupport(IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            Util.AppConfigHelper appConfigHelper = serviceProvider.GetService<Util.AppConfigHelper>();

            services.AddCors(options =>
            {
                options.AddPolicy(_CORS_POLICY_NAME, corsOptions =>
                {
                    corsOptions
                        .WithOrigins(appConfigHelper.GetValue<string>(_CORS_ALLOWED_HOST_KEY))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private void ConfigureAuthorizationPolicy(MvcOptions options)
        {
            //Build policy, that will restricate API access to only Authorized user.  
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser()
                            .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }
        #endregion
    }
}
