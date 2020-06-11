using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Util;
using SocialNetwork.WebUtil;
using System.Net;

namespace SocialNetwork.API
{
    public class Startup
    {
        private const string _CORS_POLICY_NAME = "SocialNetworkCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAppServices(Configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            AppConfigHelper appConfigHelper = serviceProvider.GetService<AppConfigHelper>();

            services.AddCors(options =>
            {
                options.AddPolicy(_CORS_POLICY_NAME, corsOptions =>
                {
                    corsOptions
                        .WithOrigins(appConfigHelper.GetValue<string>("Cors:AllowedHost"))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(TaskCanceledExceptionFilter));
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

            app.ConfigureCommonMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
