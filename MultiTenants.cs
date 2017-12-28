using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MultiTenants.Utils;

namespace MultiTenants
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            Init(host.Services).Wait();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static async Task Init(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<AppInitUtils>();
                await initializer.SeedTenant();
            }
        }
    }
}
