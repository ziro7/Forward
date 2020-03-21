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
        public IWorkExperienceService WorkExperienceService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string JobId { get; set; }
        public Job Job { get; set; }
        public List<WorkExperience> WorkExperience { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool IsSaved;

        protected override async Task OnInitializedAsync() {
            IsSaved = false;
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

            if (Job.IsValid()) {
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

                } else {
                    await JobService.UpdateJob(Job);
                    StatusClass = "alert-succes";
                    Message = "Job updated succesfully.";
                    IsSaved = true;
                    foreach (var experiences in Job.WorkExperiences) {
                        await WorkExperienceService.UpdateWorkExperience(experiences);
                    }
                }
            } else {
                StatusClass = "alert-danger";
                Message = "Job is not valid";
                IsSaved = false;
            }
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
            var newWorkExperience = new WorkExperience() {Titel="Titel", Job=Job, JobForeignKey=Job.JobId };
            await WorkExperienceService.AddWorkExperience(newWorkExperience);
            Job.WorkExperiences.Add(newWorkExperience); 
        }

        protected async Task DeleteWorkExperience(WorkExperience experience) {
            Job.WorkExperiences.Remove(Job.WorkExperiences.Find(w => w.Id == experience.Id));
            await WorkExperienceService.DeleteWorkExperience(experience.Id);
        }

    }
}
