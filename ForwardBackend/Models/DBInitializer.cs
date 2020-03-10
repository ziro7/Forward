using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForwardBackend.Models
{
    public class DBInitializer
    {
        public static void Seed(AppDbContext context) {

            if (!context.Jobs.Any()) {
                context.AddRange(
                    new Job { JobId = 1, CompanyName = "SimCorp", StartDate = new DateTime(2018,12,01), EndDate = new DateTime(2020,12,01), WorkExperiences=new List<WorkExperience>() { 
                        new WorkExperience { Id = 1, Titel = "Developer", Description = "IBOR Art", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) } } 
                    },
                    new Job {
                        JobId = 2, CompanyName = "Testhuset", StartDate = new DateTime(2017, 01, 01), EndDate = new DateTime(2018, 12, 01), WorkExperiences = new List<WorkExperience>() {
                        new WorkExperience { Id = 2, Titel = "Tester", Description = "Landbrugsstyrelsen", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) },
                        new WorkExperience { Id = 3, Titel = "Test Manager", Description = "Skandia", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) },
                        new WorkExperience { Id = 4, Titel = "Tester", Description = "DGD & EnergiNet", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) }}
                    },
                    new Job {JobId = 3, CompanyName = "Schantz", StartDate = new DateTime(2015, 05, 01), EndDate = new DateTime(2017, 01, 01), WorkExperiences = new List<WorkExperience>() {
                        new WorkExperience { Id = 5, Titel = "Tester", Description = "Landbrugsstyrelsen", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) }}
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
