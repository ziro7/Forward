using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Linq;

namespace Route.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _httpClient;

        public IntegrationTest() {

            var appFactory = new WebApplicationFactory<Startup>();
            _httpClient = appFactory.CreateClient();
        }
    }
}
