using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Validation;

namespace Core
{
    public class Job : IValidatableObject
    {
        public int JobId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Company name is too long")]
        public string CompanyName { get; set; }
        [Required]
        [StartDateValidator(myBirthday = "12-10-1982")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ValidateComplexType]
        public List<WorkExperience> WorkExperiences { get; set; }

        public bool IsValid() {
            return IsDatesValid();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            var jobErrors = new List<ValidationResult>();

            if (WorkExperiences != null && WorkExperiences.Count > 0) {
                foreach (var experience in WorkExperiences) {
                    if (experience.FromDate < StartDate) {
                        jobErrors.Add(new ValidationResult("The start date of the job function can't be before the start date of the job.", new[] { nameof(experience.FromDate) }));
                    }
                    if (experience.EndDate > EndDate) {
                        jobErrors.Add(new ValidationResult("The end date of the job function can't be before the end date of the job.", new[] { nameof(experience.EndDate) }));
                    }
                }
            }
            return jobErrors;
        }

        // Deprecated - TODO change unit test to the new Validation reslt.
        private bool IsDatesValid() {
            if (WorkExperiences != null && WorkExperiences.Count>0) {
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
