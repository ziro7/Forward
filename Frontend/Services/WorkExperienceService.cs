using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public Task<WorkExperience> AddWorkExperience(WorkExperience workexperience) {
            throw new NotImplementedException();
        }

        public Task DeleteWorkExperience(int workexperienceId) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkExperience>> GetAllWorkExperiences() {
            return await JsonSerializer.DeserializeAsync<IEnumerable<WorkExperience>>(
                await _httpClient.GetStreamAsync($"api/WorkExperiences"), options);
        }

        public async Task<WorkExperience> GetWorkExperience(int workexperienceId) {
            return await JsonSerializer.DeserializeAsync<WorkExperience>(
                await _httpClient.GetStreamAsync($"api/WorkExperiences/{workexperienceId}"), options);
        }

        public Task UpdateWorkExperience(WorkExperience workexperience) {
            throw new NotImplementedException();
        }
    }
}
