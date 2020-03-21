using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core
{
    public class Job
    {
        public int JobId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Company name is too long")]
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }

        public bool IsValid() {
            return IsDatesValid();
        }

        private bool IsDatesValid() {
            if (WorkExperiences.Count != null && WorkExperiences.Count>0) {
                foreach (var experience in WorkExperiences) {
                    if (experience.FromDate < StartDate) { return false; }
                    if (experience.EndDate > EndDate) { return false; }
                }
                return true;
            } else {
                return true;
            }
        }
    }
}
