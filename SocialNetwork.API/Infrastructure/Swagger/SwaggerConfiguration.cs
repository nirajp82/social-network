using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace SocialNetwork.API
{
    public static class SwaggerConfiguration
    {
        #region Members
        private const string _docName = "SocialNetwork";
        #endregion


        #region Public Methods
        public static void ConfigureSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc(_docName, new OpenApiInfo
                {
                    Title = "SocialNetwork API",
                    Version = "Version 1",
                    Description = "A simple social network API",
                    Contact = new OpenApiContact
                    {
                        Name = "NPatel",
                        Url = new Uri("https://github.com/nirajp82/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "The MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
            });
        }

        public static void ConfigureSwaggerMiddleware(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{_docName}/swagger.json", "SocialNetwork API");
            });
        } 
        #endregion
    }
}