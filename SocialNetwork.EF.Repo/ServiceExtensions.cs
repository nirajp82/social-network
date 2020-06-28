using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace SocialNetwork.EF.Repo
{
    public static class ServiceExtensions
    {
        #region Member
        static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        #endregion


        #region Extension Method
        public static void ConfigureRepoServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add DBContextPool will register ApplicationContext type IOC container, with a scoped lifetime by default.
            //This is safe from concurrent access issues in most ASP.NET Core applications because there is only one thread 
            //executing each client request at a given time, and because each request gets a separate dependency injection scope
            //(and therefore a separate DbContext instance).
            //services.AddDbContextPool<ApplicationContext>(optionBuilder =>

            //Using DBContext as, injecting service in to ApplicationContext Constructor.
            services.AddDbContext<ApplicationContext>(optionBuilder =>
            {
#if DEBUG
                   optionBuilder = optionBuilder.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();  //tie-up DbContext with LoggerFactory object
#endif
                   //connection resiliency
                   optionBuilder.UseSqlServer
                        (
                            configuration.GetConnectionString("SocialNetwork"),
                            options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)
                        );
               });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        #endregion
    }
}
