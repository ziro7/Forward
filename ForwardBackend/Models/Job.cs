using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForwardBackend.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
    }
}
