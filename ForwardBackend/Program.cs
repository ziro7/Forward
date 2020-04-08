using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForwardBackend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ForwardBackend
{
    public class Program
    {
        public static void Main(string[] args) {
            //CreateHostBuilder(args).Build().Run();
            // Above is replaced with below code to seed the datebase after build but before run.

            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<AppDbContext>();
                    DBInitializer.Seed(context);
                    logger.LogInformation("Seeding database if empty");  
                } catch (InvalidOperationException ex){
                    logger.LogWarning(LoggingEvents.SystemEvent, ex, "An error occurred getting the context.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => {
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(
                        "http://localhost:5000", //also specified in launch settings - front end is 5002 and 5003
                        "https://localhost:5001");
                });
    }
}
