using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
        [JsonIgnore] //causes cyclic serialization if not ignored in json.
        public Job Job { get; set; } 
    }
}
