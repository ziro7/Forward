using System;
using System.IO;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace Forward
{
    public class AuthConfig
    {
        public string Instance { get; set; } ="https://login.microsoftonline.com/{0}";
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string Authority {
            get {
                return String.Format(CultureInfo.InvariantCulture, Instance, TenantId);
            }
        }
        public string ClientSecret { get; set; }
        public string BaseAddress { get; set; }
        public string ResourceID { get; set; }

        public static AuthConfig ReadFromJsonFile(string path) {

            // The method basicly gets the data from appsettings.json fil and stores them in a configuration.
            IConfiguration Configuration;

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile(path);

            Configuration = builder.Build();

            return Configuration.Get<AuthConfig>();
        }
    }
}
