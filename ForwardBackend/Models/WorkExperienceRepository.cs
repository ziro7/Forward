using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace ForwardBackend.Models
{
    public class WorkExperienceRepository : IWorkExperienceRepository
    {
        private readonly AppDbContext _appDbContext;

        public WorkExperienceRepository(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public IEnumerable<WorkExperience> GetAllWorkExperiences() {
            return _appDbContext.WorkExperiences;
        }

        public IEnumerable<WorkExperience> GetAllWorkExperiencesForAJob(int jobId) {
            return _appDbContext.WorkExperiences.Where(w => w.JobForeignKey == jobId);
        }

        public WorkExperience GetWorkExperienceById(int workId) {
            return _appDbContext.WorkExperiences.FirstOrDefault(w => w.Id == workId);
        }
    }
}
