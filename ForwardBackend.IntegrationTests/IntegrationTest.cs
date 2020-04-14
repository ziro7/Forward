using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Forward.Services.Tokens;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Identity.Client;

namespace ForwardBackend.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _httpClient;

        public IntegrationTest() {
            var appFactory = new WebApplicationFactory<Startup>();
            _httpClient = appFactory.CreateClient();
        }

        protected async Task Authenticate() {
            var authResult = await AccessTokenForwardBackend.GetAuthenticationResult();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
        }

    }
}
