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
                    new Job { CompanyName = "SimCorp", StartDate = new DateTime(2018,12,01), EndDate = new DateTime(2020,12,01), WorkExperiences=new List<WorkExperience>() { 
                        new WorkExperience { Titel = "Developer", Description = "IBOR Art", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) } } 
                    },
                    new Job {
                        CompanyName = "Testhuset", StartDate = new DateTime(2017, 01, 01), EndDate = new DateTime(2018, 12, 01), WorkExperiences = new List<WorkExperience>() {
                        new WorkExperience { Titel = "Tester", Description = "Landbrugsstyrelsen", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) },
                        new WorkExperience { Titel = "Test Manager", Description = "Skandia", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) },
                        new WorkExperience { Titel = "Tester", Description = "DGD & EnergiNet", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) }}
                    },
                    new Job {CompanyName = "Schantz", StartDate = new DateTime(2015, 05, 01), EndDate = new DateTime(2017, 01, 01), WorkExperiences = new List<WorkExperience>() {
                        new WorkExperience { Titel = "Tester", Description = "Landbrugsstyrelsen", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) }}
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
