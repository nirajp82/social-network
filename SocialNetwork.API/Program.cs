using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SocialNetwork.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Build configuration by reading "appsettings.json" file.
            IConfiguration configuration = new ConfigurationBuilder()
                                         .AddJsonFile("appsettings.json", false)
                                         .Build();

            Log.Logger = new LoggerConfiguration()
                            .ReadFrom
                            .Configuration(configuration)
                            .CreateLogger();
            try
            {
                Log.Information("Application starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start correctly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()//Use Serilog as logger instead of built in .net core logger.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
