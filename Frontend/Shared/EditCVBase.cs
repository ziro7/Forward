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
        public List<WorkExperience> WorkExperience { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool IsSaved;
        protected bool SubmitPressed;

        protected override async Task OnInitializedAsync() {
            IsSaved = false;
            SubmitPressed = false;
            int.TryParse(JobId, out int jobId);

            if (jobId == 0) {
                // As the parameter is 0 the page was not entered with a "known" job - hence a new should be created.
                // placing som default values.
                WorkExperience = new List<WorkExperience> { new WorkExperience {
                                            Titel = "Titel",
                                            Description = "What does your job involes?",
                                            FromDate = new DateTime(2020, 05, 01),
                                            EndDate = new DateTime(2020, 12, 01)
                                        } };
                Job = new Job { CompanyName = "Company", StartDate = new DateTime(2020, 05, 01), EndDate = new DateTime(2020, 12, 01), WorkExperiences = WorkExperience };
                ;
            } else {
                // The job is known - get it.
                Job = await JobService.GetJob(int.Parse(JobId));
            }

        }

        protected async Task HandleValidSubmit() {

            if (Job.JobId == 0) {
                var newJob = await JobService.AddJob(Job);
                if (newJob != null) {
                    StatusClass = "alert-success";
                    Message = "New Job Added successfully.";
                    IsSaved = true;
                } else {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new Job. Please try again.";
                    IsSaved = false;
                }

            } else if (SubmitPressed){
                await JobService.UpdateJob(Job);
                StatusClass = "alert-succes";
                Message = "Job updated succesfully.";
                IsSaved = true;
            }
        }

        protected void SaveJob() {
            SubmitPressed = true;
        }

        protected async Task DeleteJob() {
            await JobService.DeleteJob(Job.JobId);
            StatusClass = "alert-succes";
            Message = "Deleted succedfully.";
            IsSaved = true;
        }

        protected void NavigationToOverview() {
            NavigationManager.NavigateTo("/WhoAmI");
        }
        protected async Task AddWorkExperience() {
            var newWorkExperience = new WorkExperience() {Titel="Titel", FromDate=Job.StartDate, EndDate=Job.EndDate, Description="What was the job function?", Job=Job, JobForeignKey=Job.JobId };
            Job.WorkExperiences.Add(newWorkExperience);
            await JobService.UpdateJob(Job);
            StatusClass = "alert-succes";
            Message = "Workexperience added to job.";
        }

        protected async Task DeleteWorkExperience(WorkExperience experience) {
            Job.WorkExperiences.Remove(Job.WorkExperiences.Find(w => w.Id == experience.Id));
            await JobService.UpdateJob(Job);
            StatusClass = "alert-succes";
            Message = "Workexperience deleted from job.";
        }

    }
}
