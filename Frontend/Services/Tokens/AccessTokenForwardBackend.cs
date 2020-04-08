using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Forward.Services.Tokens
{
    public static class AccessTokenForwardBackend
    {
        public static async Task<AuthenticationResult> GetAccessToken() {
            // Bad practices to store these data in appsettings - should be in a keyvault or some other secret place.
            // As this is just practise i allow it to be stored there for the time being.
            // Use AuthConfig class to create my authorization configuration.
            AuthConfig config = AuthConfig.ReadFromJsonFile("appsettings.json");

            // The IConfidentialClientApplication is used to create an application with the configuration we just set up.
            // When the app is build, it can be used to aquire a token.
            IConfidentialClientApplication app;

            app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                .WithClientSecret(config.ClientSecret)
                .WithAuthority(new Uri(config.Authority))
                .Build();

            // Currently this could just be a string as i only have 1 resoruce, but maybe i will add more later.
            string[] ResourceIds = new string[] { config.ResourceID };

            AuthenticationResult result = null;
            try {
                result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
            } catch (MsalClientException ex) {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
