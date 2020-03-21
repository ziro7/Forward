using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core;

namespace Forward.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        public readonly HttpClient _httpClient;
        JsonSerializerOptions options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        };

        public WorkExperienceService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task<WorkExperience> AddWorkExperience(WorkExperience workexperience) {

            var workExperienceJson = new StringContent(JsonSerializer.Serialize(workexperience), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/WorkExperiences",workExperienceJson);

            if (response.IsSuccessStatusCode) {
                return await JsonSerializer.DeserializeAsync<WorkExperience>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }

        public async Task DeleteWorkExperience(int workexperienceId) {
            await _httpClient.DeleteAsync($"api/WorkExperiences/{workexperienceId}");
        }

        public async Task<IEnumerable<WorkExperience>> GetAllWorkExperiences() {
            return await JsonSerializer.DeserializeAsync<IEnumerable<WorkExperience>>(
                await _httpClient.GetStreamAsync("api/WorkExperiences"), options);
        }

        public async Task<WorkExperience> GetWorkExperience(int workexperienceId) {
            return await JsonSerializer.DeserializeAsync<WorkExperience>(
                await _httpClient.GetStreamAsync($"api/WorkExperiences/{workexperienceId}"), options);
        }

        public async Task UpdateWorkExperience(WorkExperience workexperience) {
            var workExperienceJson = new StringContent(JsonSerializer.Serialize(workexperience), Encoding.UTF8, "application/json");
            int workExperienceId = workexperience.Id;
            await _httpClient.PutAsync($"api/WorkExperiences/{workExperienceId}", workExperienceJson);
        }
    }
}
