using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SportsStore.WebMvcApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseDefaultServiceProvider(opts => opts.ValidateScopes = false)
                        .UseStartup<Startup>();
                });
        }
    }
}
