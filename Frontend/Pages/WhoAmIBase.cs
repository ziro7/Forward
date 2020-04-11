using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Forward.Components;
using Forward.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Forward.Pages
{
    public class WhoAmIBase : ComponentBase
    {
        [Inject]
        public IJobService JobService { get; set; }

        protected AddJobDialogBase AddJobDialog{ get; set; }
        public Job[] Jobs { get; set; }

        // One of Blazors functions which will be called when the components is going to be initiallized. Stated on ComponenBase
        protected override async Task OnInitializedAsync() {
            Jobs = await JobService.GetAllJobs();
        }

        public async void AddJobDialog_OnDialogClose() {
            Jobs = await JobService.GetAllJobs();
            StateHasChanged();
        }

        protected void AddNewJob() {
            AddJobDialog.Show();
        }
    }
}
