﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace ForwardBackend.Models
{
    interface IWorkExperienceRepository
    {
        IEnumerable<WorkExperience> GetAllWorkExperiences();
        WorkExperience GetWorkExperienceById(int workId);

        IEnumerable<WorkExperience> GetAllWorkExperiencesForAJob(int jobId);
    }
}