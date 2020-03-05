using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Forward.Pages
{
    public class WhoAmIBase : ComponentBase
    {
        //public List<WorkExperience> WorkExperience { get; set; } // TODO Make it to a class with the relevant data for a workexperience.

        // One of Blazors functions which will be called when the components is going to be initiallized. Stated on ComponenBase
        protected override Task OnInitializedAsync() {
            InitializeWorkExperience();
            return base.OnInitializedAsync();
        }

        private void InitializeWorkExperience() {
            //WorkExperience = new List<WorkExperience>();
            //WorkExperience.Add(new WorkExperience("SimCorp", "Developer", "Developer in IBOR Art", new DateTime(2018, 12, 1), DateTime.Now));
            //WorkExperience.Add(new WorkExperience("Testhust", "Tester", "Manual tester and test manager", new DateTime(2017, 5, 1), new DateTime(2018,12,1)));
        }

    }
}
