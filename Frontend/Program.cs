using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Forward.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Forward.Data;

namespace Forward
{
    public class Program
    {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {
                var serviceProvider = scope.ServiceProvider;
                try {
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    MyIdentityDataInitializer.SeedUsers(userManager);
                } catch (Exception ex) {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    // States which class should be used to handle startup.
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(
                        "http://localhost:5002", //also specified in launch settings - backend API is 5000 and 5001 
                        "https://localhost:5003");
                });
    }
}
