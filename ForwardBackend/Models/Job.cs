using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Validation;

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
