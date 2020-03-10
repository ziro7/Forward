using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core;

namespace Forward.Services
{
    public class JobService : IJobsService
    {
        public readonly HttpClient _httpClient;

        public JobService() {

        }
        public Task<Job> AddJob(Job job) {
            throw new NotImplementedException();
        }

        public Task DeleteJob(int jobId) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Job>> GetAllJobs() {
            throw new NotImplementedException();
        }

        public Task<Job> GetJob(int jobId) {
            throw new NotImplementedException();
        }

        public Task UpdateJob(Job job) {
            throw new NotImplementedException();
        }
    }
}
