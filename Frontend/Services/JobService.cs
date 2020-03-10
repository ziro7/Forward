using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Core;

namespace Forward.Services
{
    public class JobService : IJobService
    {
        public readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public Task<Job> AddJob(Job job) {
            throw new NotImplementedException();
        }

        public Task DeleteJob(int jobId) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Job>> GetAllJobs() {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Job>>
                (await _httpClient.GetStreamAsync($"api/jobs"), new JsonSerializerOptions());
        }

        public async Task<Job> GetJob(int jobId) {
            return await JsonSerializer.DeserializeAsync<Job>
                (await _httpClient.GetStreamAsync($"api/jobs/{jobId}"), new JsonSerializerOptions());
        }

        public Task UpdateJob(Job job) {
            throw new NotImplementedException();
        }
    }
}
