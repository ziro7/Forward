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
        public List<WorkExperience> WorkExperiences { get; }

        public Job(int jobid, string companyName, DateTime startDate, DateTime endDate) {
            JobId = jobid;
            CompanyName = companyName;
            StartDate = startDate;
            EndDate = endDate;
            WorkExperiences = new List<WorkExperience>();
        }

        public Job() {
            WorkExperiences = new List<WorkExperience>();
        }
        /*
         * Why is this here and not the Core model? - Well apperently the ValidateComplexTypeAttribute used in the blazor 
         * validation causes a "System.InvalidOperationException: ValidateComplexTypeAttribute can only used with ObjectGraphDataAnnotationsValidator." 
         * if used in the API part - so had to seperate them and remote the attribute.
         * https://github.com/dotnet/aspnetcore/issues/17316
         */
    }
}
