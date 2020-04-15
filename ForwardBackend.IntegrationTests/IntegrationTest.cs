using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Forward.Services.Tokens;
using ForwardBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Client;
using System.Linq;

namespace ForwardBackend.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _httpClient;

        public IntegrationTest() {

            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => {
                    builder.ConfigureServices(services => {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                        if(descriptor!= null) {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<DataContext>(options => {
                            options.UseInMemoryDatabase("ForwardTest");
                        });

                        var sp = services.BuildServiceProvider();
                    });
                });
            _httpClient = appFactory.CreateClient();
        }

        protected async Task Authenticate() {
            var authResult = await AccessTokenForwardBackend.GetAuthenticationResult();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
        }

        protected async Task<Job> AddJobToDatabase(Job job) {

            await Authenticate();
            var jobJson = new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/jobs", jobJson);
            if (response.IsSuccessStatusCode) {
                return await JsonSerializer.DeserializeAsync<Job>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }

        protected async Task DeleteJobInDatabase(int jobId) {

            await Authenticate();
            await _httpClient.DeleteAsync($"api/jobs/{jobId}");
        }
    }
}
