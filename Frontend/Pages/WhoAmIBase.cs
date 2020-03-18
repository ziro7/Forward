using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Forward.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Forward.Pages
{
    public class WhoAmIBase : ComponentBase
    {
        [Inject]
        public IJobService JobService { get; set; }
        [Inject]
        public IWorkExperienceService WorkExperienceService { get; set; }
        public Job[] Jobs { get; set; }

        // One of Blazors functions which will be called when the components is going to be initiallized. Stated on ComponenBase
        protected override async Task OnInitializedAsync() {
            Jobs = await JobService.GetAllJobs();
        }
    }
}
