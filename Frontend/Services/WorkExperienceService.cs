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
using Microsoft.Identity.Client;

namespace Forward.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        public readonly HttpClient _httpClient;
        private AuthenticationResult _authResult;
        readonly JsonSerializerOptions options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        };

        public WorkExperienceService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task<WorkExperience> AddWorkExperience(WorkExperience workexperience) {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);

                var workExperienceJson = new StringContent(JsonSerializer.Serialize(workexperience), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/WorkExperiences", workExperienceJson);

                if (response.IsSuccessStatusCode) {
                    return await JsonSerializer.DeserializeAsync<WorkExperience>(await response.Content.ReadAsStreamAsync());
                }
                return null;
            }
            return null;
        }

        public async Task DeleteWorkExperience(int workexperienceId) {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);
                await _httpClient.DeleteAsync($"api/WorkExperiences/{workexperienceId}");
            }
        }

        public async Task<IEnumerable<WorkExperience>> GetAllWorkExperiences() {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);
                return await JsonSerializer.DeserializeAsync<IEnumerable<WorkExperience>>(
                await _httpClient.GetStreamAsync("api/WorkExperiences"), options);
            }
            return null;
        }

        public async Task<WorkExperience> GetWorkExperience(int workexperienceId) {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);
                return await JsonSerializer.DeserializeAsync<WorkExperience>(
                await _httpClient.GetStreamAsync($"api/WorkExperiences/{workexperienceId}"), options);
            }
            return null;
        }

        public async Task UpdateWorkExperience(WorkExperience workexperience) {
            await GetAccessTokenIfNotKnow();

            if (!string.IsNullOrEmpty(_authResult.AccessToken)) {
                AddBearerTokenToRequestHeader(_authResult);
                var workExperienceJson = new StringContent(JsonSerializer.Serialize(workexperience), Encoding.UTF8, "application/json");
                int workExperienceId = workexperience.Id;
                await _httpClient.PutAsync($"api/WorkExperiences/{workExperienceId}", workExperienceJson);
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
    }
}
