using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace ForwardBackend.Models
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        Job GetJobById(int jobId);


    }
}
