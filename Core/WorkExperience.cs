using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        // Below is used for Entity Framework to set up the relationship.
        public int JobForeignKey { get; set; }
        public Job Job { get; set; } 
    }
}
