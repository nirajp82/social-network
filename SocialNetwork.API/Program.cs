using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SocialNetwork.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // Remove Server header from response.
                    //webBuilder.UseKestrel(x => x.AddServerHeader = false);
                });
    }
}
