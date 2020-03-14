﻿using System;
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
        public Job job { get; set; }
        public WorkExperience workExperience { get; set; }

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

        public void HandleValidSubmit() {
            //workExperience = new WorkExperience { }
            //job = new Job { CompanyName = CompanyName  StartDate }
            //JobService.AddJob()
            //TODO ideer - Authorize, Deploy, Validation some logic like dates - where? - make some input fields to server logic (hacker ranks)


        }

    }
}