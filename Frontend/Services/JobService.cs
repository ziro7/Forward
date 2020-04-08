using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core;
using Forward.Services.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace Forward.Services
{
    public class JobService : IJobService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthenticationResult _authResult;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        public JobService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) {
            _httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            _httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Job> AddJob(Job job) {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);

                var jobJson = new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/jobs", jobJson);

                if (response.IsSuccessStatusCode) {
                    return await JsonSerializer.DeserializeAsync<Job>(await response.Content.ReadAsStreamAsync());
                }
                return null;
            } else {
                return null;
            }
        }

        private async Task GetAccessTokenIfNotKnow() {
            if (_authResult == null) { 
                _authResult = await AccessTokenForwardBackend.GetAccessToken(); 
            }
        }

        private void AddBearerTokenToRequestHeader(AuthenticationResult authResult) {
            var defaultRequestHeaders = _httpClient.DefaultRequestHeaders;

            if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json")) {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
        }

        public async Task DeleteJob(int jobId) {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);

                await _httpClient.DeleteAsync($"api/jobs/{jobId}");
            } 
        }

        public async Task<Job[]> GetAllJobs() {
            await GetAccessTokenIfNotKnow();
            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);

                var jobsFromJson = await JsonSerializer.DeserializeAsync<IEnumerable<Job>>(
                //await _httpClient.GetStreamAsync($"api/jobs?$OrderBy=StartDate"), options); //Using OData functionality to order data. -> not supported in 3.0 yet
                await _httpClient.GetStreamAsync($"api/jobs"), _options);

                return jobsFromJson.ToArray();

            }

            return null;
        }

        public async Task<Job> GetJob(int jobId) {

            await GetAccessTokenIfNotKnow();
            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);

                var result = await JsonSerializer.DeserializeAsync<Job>
                (await _httpClient.GetStreamAsync($"api/jobs/{jobId}"), _options);

                return result;
            }
            return null;
        }

        public async Task UpdateJob(Job job) {
            await GetAccessTokenIfNotKnow();
            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);

                var jobJson =
                new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
                int jobId = job.JobId;
                await _httpClient.PutAsync($"api/jobs/{jobId}", jobJson);
            }
        }
    }
}
