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
using System.IO;
using Microsoft.Extensions.FileProviders;
using System;
using Serilog;

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
            app.UseHsts();


            ConfigureSecuriyHeaderMiddleware(app);

            // Enables default file mapping on the current path
            app.UseDefaultFiles();

            //Enables static file serving for the current request path
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            });

            //Log request details
            app.UseSerilogRequestLogging();

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

                //Route all not matching request to Fallback controller.
                endpoints.MapFallbackToController("Index", "Fallback");
            });

            //, IApplicationLifetime lifetime
            //lifetime.ApplicationStarted.Register(OnApplicationStarted);
            //lifetime.ApplicationStopping.Register(OnShutdown);
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
                        .WithExposedHeaders("WWW-Authenticate")
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

        private void ConfigureSecuriyHeaderMiddleware(IApplicationBuilder app)
        {
            ConfigSettings configSettings = app.ApplicationServices.GetService<ConfigSettings>();

            //Sets the X-Content-Type-Options response header. 
            //This is marker used by the server to indicate that the MIME types advertised in the Content-Type 
            //headers should not be changed and be followed.
            app.UseXContentTypeOptions();

            //Sets the Referrer-Policy response header.
            //It instructs the browser, when navigating to the target resource, to omit the Referer header
            app.UseReferrerPolicy(opt => opt.NoReferrer());

            //Sets the X-Xss-Protection header
            //It instructs the browser to prevent rendering of the page if an attack is detected.
            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());

            //Sets the X-Frame-Options response header.
            //The X-Frame-Options HTTP response header can be used to indicate whether or not a browser should be allowed to
            //render a page in a <frame>, <iframe>, <embed> or <object>. Sites can use this to avoid click-jacking attacks, 
            //by ensuring that their content is not embedded into other sites.
            app.UseXfo(opt => opt.Deny());

            //Sets the Content-Security-Policy-Report-Only response header
            app.UseCsp(opt => opt
                .BlockAllMixedContent()
                .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com",
                                  "sha256-F4GpCPyRepgP5znjMD8sc7PEjzet5Eef4r09dEGPpTs="))
                .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com", "data:"))
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self().CustomSources(configSettings.BlobAccountBaseUri, "blob:", "data:"))
                //Allow Inlinescript by providing hash
                .ScriptSources(s => s.Self().CustomSources("sha256-BEfVagb2tFvpT8eok5d+XlOLrZ/j3XC6FcyYKtUlaWQ="))
            );
        }

        //public void OnApplicationStarted()
        //{
        //    Console.Out.WriteLine($"Open Api Started");
        //}

        //public void OnShutdown()
        //{
        //    Console.Out.WriteLine($"Open Api is shutting down.");
        //}
        #endregion
    }
}
