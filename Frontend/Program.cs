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

namespace Forward
{
    public class Program
    {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    // States which class should be used to handle startup.
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(
                        "http://localhost:5002", //also specified in launch settings - API is 5000 and 5001
                        "https://localhost:5003");
                });
    }
}
