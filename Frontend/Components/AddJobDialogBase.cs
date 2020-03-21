using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Forward.Services;
using Microsoft.AspNetCore.Components;

namespace Forward.Components
{
    public class AddJobDialogBase : ComponentBase
    {
        [Inject]
        public IJobService JobService { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        public Job NewJob { get; set; } = new Job { CompanyName = "Company Name" };
        public bool ShowDialog { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool IsSaved;

        public void Show() {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged(); // This triggers Blazor to valuate if the DOM need to be updated
        }

        public void Close() {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog() {
            NewJob = new Job() { CompanyName = "Company Name" };
        }

        protected async Task HandleValidSubmit() {

            var newJob = await JobService.AddJob(NewJob);
            if (newJob != null) {
                StatusClass = "alert-success";
                Message = "New Job Added successfully.";
                IsSaved = true;
                await CloseEventCallback.InvokeAsync(true);
                ShowDialog = false;

            } else {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new Job. Please try again.";
                IsSaved = false;
            } 
            
            StateHasChanged();
        }
    }
}
