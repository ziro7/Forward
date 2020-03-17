using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Forward.Services;
using Microsoft.AspNetCore.Components;

namespace Forward.Shared
{
    public class EditCVBase : ComponentBase
    {
        [Inject]
        public IJobService JobService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string JobId { get; set; }
        public Job Job { get; set; }
        public List<WorkExperience> workExperience { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool IsSaved;

        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string CompanyName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Titel { get; set; }
        [Required]
        public DateTime WorkStartDate { get; set; }
        [Required]
        public DateTime WorkEndDate { get; set; }

        [StringLength(200, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }



        protected override async Task OnInitializedAsync() {
            IsSaved = false;
            int jobId;
            int.TryParse(JobId, out jobId);

            if (jobId == 0) {
                // As the parameter is 0 the page was not entered with a "known" job - hence a new should be created.
                // placing som default values.
                workExperience = new List<WorkExperience> { new WorkExperience {
                                            Titel = "Titel",
                                            Description = "What does your job involes?",
                                            FromDate = new DateTime(2020, 05, 01),
                                            EndDate = new DateTime(2020, 12, 01)
                                        } };
                Job = new Job { CompanyName = "Company", StartDate = new DateTime(2020, 05, 01), EndDate = new DateTime(2020, 12, 01), WorkExperiences = workExperience };
                ;
            } else {
                // The job is known - get it.
                Job = await JobService.GetJob(int.Parse(JobId));
            }

        }

        protected async Task HandleValidSubmit() {
            if (Job.JobId == 0) {
                var newJob = await JobService.AddJob(Job);
                if(newJob != null) {
                    StatusClass = "alert-success";
                    Message = "New Job Added successfully.";
                    IsSaved = true;
                } else {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new Job. Please try again.";
                    IsSaved = false;
                }

            } else {
                await JobService.UpdateJob(Job);
                StatusClass = "alert-succes";
                Message = "Job updated succesfully.";
                IsSaved = true;
            } 

            //workExperience = new WorkExperience { }
            //job = new Job { CompanyName = CompanyName  StartDate }
            //JobService.AddJob()
            //TODO ideer - Authorize, Deploy, Validation some logic like dates - where? - make some input fields to server logic (hacker ranks)
        }

        protected async Task DeleteJob() {
            await JobService.DeleteJob(Job.JobId);
            StatusClass = "alert-succes";
            Message = "Deleted succedfully.";
            IsSaved = true;
        }

        protected void NavigateToOverview() {
            NavigationManager.NavigateTo("/WhoAmI");
        }

    }
}
