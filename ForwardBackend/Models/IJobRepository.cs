using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForwardBackend.Models
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        Job GetJobById(int workId);
    }
}
