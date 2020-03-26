using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Forward.Services;
using Microsoft.AspNetCore.Components;

namespace Forward.Pages
{
    public class CVDetailBase : ComponentBase
    {
        [Parameter]
        public int JobId { get; set; }
        [Inject]
        public IJobService JobService { get; set; }
        public Job Job { get; set; } = new Job();

        protected override async Task OnInitializedAsync() {
            Job = await JobService.GetJob(JobId);
        }
    }
}
