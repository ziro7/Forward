using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace Forward.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        public Task<WorkExperience> AddWorkExperience(WorkExperience workexperience) {
            throw new NotImplementedException();
        }

        public Task DeleteWorkExperience(int workexperienceId) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkExperience>> GetAllWorkExperiences() {
            throw new NotImplementedException();
        }

        public Task<WorkExperience> GetWorkExperience(int workexperienceId) {
            throw new NotImplementedException();
        }

        public Task UpdateWorkExperience(WorkExperience workexperience) {
            throw new NotImplementedException();
        }
    }
}
