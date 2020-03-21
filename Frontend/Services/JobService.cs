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
    public class JobService : IJobService
    {
        public readonly HttpClient _httpClient;
        readonly JsonSerializerOptions options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };

        public JobService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<Job> AddJob(Job job) {
            var jobJson = new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/jobs", jobJson);

            if (response.IsSuccessStatusCode) {
                return await JsonSerializer.DeserializeAsync<Job>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }

        public async Task DeleteJob(int jobId) {
            await _httpClient.DeleteAsync($"api/jobs/{jobId}");
        }

        public async Task<Job[]> GetAllJobs() {

            List<Job> jobs = new List<Job>();
            await Task.Delay(2000);
            
            var jobsFromJson = await JsonSerializer.DeserializeAsync<IEnumerable<Job>>(
                //await _httpClient.GetStreamAsync($"api/jobs?$OrderBy=StartDate"), options); //Using OData functionality to order data.
                await _httpClient.GetStreamAsync($"api/jobs"), options); 

            foreach (var job in jobsFromJson) {
                List<WorkExperience> workExperience = job.WorkExperiences;
                var startDate = job.StartDate;
                var endDate = job.EndDate;
                var id = job.JobId;
                var companyName = job.CompanyName;
                Job newJob = new Job { JobId = id, CompanyName = companyName, StartDate = startDate, EndDate = endDate, WorkExperiences = workExperience };
                jobs.Add(newJob);
            }

            return jobs.ToArray();
        }

        public async Task<Job> GetJob(int jobId) {

            var result =  await JsonSerializer.DeserializeAsync<Job>
                (await _httpClient.GetStreamAsync($"api/jobs/{jobId}"), options);

            List<WorkExperience> workExperience = result.WorkExperiences;
            var startDate = result.StartDate;
            var endDate = result.EndDate;
            var id = result.JobId;
            var companyName = result.CompanyName;
            Job jobInFocus = new Job { JobId = id, CompanyName = companyName, StartDate = startDate, EndDate = endDate, WorkExperiences = workExperience };

            return jobInFocus;
        }

        public async Task UpdateJob(Job job) {
            var jobJson =
            new StringContent(JsonSerializer.Serialize(job), Encoding.UTF8, "application/json");
            int jobId = job.JobId;
            await _httpClient.PutAsync($"api/jobs/{jobId}", jobJson);
        }
    }
}
