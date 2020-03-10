using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace ForwardBackend.Models
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _appDbContext;

        public JobRepository(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Job> GetAllJobs() {
            // Returns all pies as there is no criteria on 
            return _appDbContext.Jobs;
        }

        public Job GetJobById(int jobId) {
            // Only return first job that have the supplied Id.
            return _appDbContext.Jobs.FirstOrDefault(j => j.JobId == jobId);
        }
    }
}
