using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace Forward.Services
{
    interface IWorkExperienceService
    {
        Task<IEnumerable<WorkExperience>> GetAllWorkExperiences();
        Task<WorkExperience> GetWorkExperience(int workexperienceId);
        Task<WorkExperience> AddWorkExperience(WorkExperience workexperience);
        Task UpdateWorkExperience(WorkExperience workexperience);
        Task DeleteWorkExperience(int workexperienceId);
    }
}
