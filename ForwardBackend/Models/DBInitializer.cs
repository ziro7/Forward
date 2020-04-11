using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace ForwardBackend.Models
{
    public static class DBInitializer
    {
        public static void Seed(AppDbContext context) {

            if (context != null && !context.Jobs.Any()) {

                var job1 = new Job { CompanyName = "SimCorp", StartDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01) };
                var job2 = new Job { CompanyName = "Testhuset", StartDate = new DateTime(2017, 01, 01), EndDate = new DateTime(2018, 12, 01) };
                var job3 = new Job { CompanyName = "Schantz", StartDate = new DateTime(2015, 05, 01), EndDate = new DateTime(2017, 01, 01) };
                var job4 = new Job { CompanyName = "Danica Pension", StartDate = new DateTime(2011, 07, 01), EndDate = new DateTime(2015, 05, 01) };
                var job5 = new Job { CompanyName = "PFA Pension", StartDate = new DateTime(2007, 08, 01), EndDate = new DateTime(2011, 07, 01) };

                context.AddRange(job1, job2, job3, job4, job5);
                context.SaveChanges();

                if (!context.WorkExperiences.Any()) {
                    var workExperience1 = new WorkExperience {
                        Titel = "Developer",
                        Description = "Arbejdet i teamet ”Hakuna Matata” som hver tredje måned tager en til flere features i et product increment planning event, hvor vi i den efterfølgende periode arbejder på disse features. Der vælges hovedsagligt features på transaktions området. Arbejdet kan indeholde opgaver i APL, C#, OCaml og ML (Meta Language) og kan være både front-end eller back-end eller en kombination (Personligt har jeg ikke arbejdet med OCaml eller ML).",
                        FromDate = new DateTime(2018, 12, 01),
                        EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job1.JobId
                    };
                    var workExperience2 = new WorkExperience {
                        Titel = "Tester", Description = "Manuel test", FromDate = new DateTime(2018, 12, 01), EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job2.JobId
                    };
                    var workExperience3 = new WorkExperience {
                        Titel = "Test Manager",
                        Description = "Koordinering af test på Digital Transformation, herunder webservices, web og grænsesystemer til kernesystemet Polaris.	Koordinering og planlægning af User accept test, Koordinering af defektprocessen samt defektmanager, Rapportering af testafvikling, udarbejdelse af testplan samt testrapport, anvendelse af TFS.",
                        FromDate = new DateTime(2018, 12, 01),
                        EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job2.JobId
                    };
                    var workExperience4 = new WorkExperience {
                        Titel = "Tester",
                        Description = "Manuel test - Anvendelse af HP alm.",
                        FromDate = new DateTime(2018, 12, 01),
                        EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job2.JobId
                    };
                    var workExperience5 = new WorkExperience {
                        Titel = "Tester & Bussines Analyst",
                        Description = "Pensionsprojektet (LPD) skulle videreudvikles og laves til et licensbaseret system. Kravspecifikation, løsningsbeskrivelse og mock - ups. Testcasedesign manuel og til integrationstest(CQ lag), Review af testcases og Testeksekvering. Eksplorativ test baseret på test charters og checklists. Defectrapportering samt gentest, Regressionstest(manuel og enkelte via Selenium Webdriver(java)). Anvendelse af Jira og Testrail.",
                        FromDate = new DateTime(2018, 12, 01),
                        EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job3.JobId
                    };
                    var workExperience6 = new WorkExperience {
                        Titel = "Tester & Kundeservicemedarbejder",
                        Description = "Forretningsudvikler samt tester på overløbspension fra Danske Bank til Danica pension. Ansvarlig for design og gennemførelse af system test samt accepttest. Anvendelse af HP QC. Ansvarlig for design og gennemførelse af system test og accepttest via HP QC af mindre opgaver som EDI og systemforbedringer. Administrativ medarbejder med arbejdsopgaver indenfor nytegninger(inkl.Basal helbredsvurdering), ændringer(§21A, PAL - fritagelse mv.), skadesager(ristorno og prorate beregninger), afgangsføring af policer.",
                        FromDate = new DateTime(2018, 12, 01),
                        EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job4.JobId
                    };
                    var workExperience7 = new WorkExperience {
                        Titel = "Pensionsmedarbejder",
                        Description = "Administrativ medarbejder med arbejdsopgaver indenfor udbetaling af invalide- og dødfaldspension, ekspedition af invalidepensioner, udarbejdelse af prognoser og depotregnskaber for invalide. Arbejdede i en periode på 1, 5 år 2 dage om ugen på et projekt, hvor der blev udarbejdet en digital platform til at hjælpe og forenkle arbejdet i rådgivningscenteret. Besvarelse af mails, udarbejdning af tilbud til kunder og behandling af problemsager. Besvarelse af mails og opkald i PFA’s rådgivningscenter.",
                        FromDate = new DateTime(2018, 12, 01),
                        EndDate = new DateTime(2020, 12, 01),
                        JobForeignKey = job5.JobId
                    };

                    context.AddRange(workExperience1, workExperience2, workExperience3, workExperience4, workExperience5, workExperience6, workExperience7);
                    context.SaveChanges();
                }
            }
        }
    }
}
