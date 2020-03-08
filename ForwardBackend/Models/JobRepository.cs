using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForwardBackend.Models
{
    public class JobRepository : IJobRepository
    {
        public IEnumerable<Job> GetAllJobs() {
            throw new NotImplementedException();
        }

        public Job GetJobById(int workId) {
            throw new NotImplementedException();
        }
    }
}
