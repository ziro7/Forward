using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForwardBackend.Models
{
    public class WorkExperience
    {
        public int WorkId { get; set; }
        public string Titel { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
