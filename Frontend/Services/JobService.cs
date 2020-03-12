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
        JsonSerializerOptions options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
        };

        public JobService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public Task<Job> AddJob(Job job) {
            throw new NotImplementedException();
        }

        public Task DeleteJob(int jobId) {
            throw new NotImplementedException();
        }

        public async Task<Job[]> GetAllJobs() {

            List<Job> jobs = new List<Job>();
            await Task.Delay(2000);
            
            var jobsFromJson = await JsonSerializer.DeserializeAsync<IEnumerable<Job>>(
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

        Task IJobService.UpdateJob(Job job) {
            throw new NotImplementedException();
        }
    }
}
