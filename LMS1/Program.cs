using LMS1.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace LMS1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                var config = webHost.Services.GetRequiredService<IConfiguration>();

                using (var innerScope = webHost.Services.CreateScope())
                {
                    var innerServices = innerScope.ServiceProvider;
                    SeedData.InitializeAsync(innerServices);
                }

                // Need to be put via the command prompt in the project directory.
                // dotnet user-secrets set "mail:AdminPW" "Az*1234"
                // Loading the password

                var adminPW = config["mail:AdminPW"];
                try
                {
                    SeedData.InitializeRoleManagement(services, adminPW).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.Message, "Seed fail");
                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
