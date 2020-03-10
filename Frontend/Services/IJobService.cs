using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace Forward.Services
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobs();
        Task<Job> GetJob(int jobId);
        Task<Job> AddJob(Job job);
        Task UpdateJob(Job job);
        Task DeleteJob(int jobId);
    }
}
