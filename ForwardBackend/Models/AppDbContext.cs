using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ForwardBackend.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) {

        }


    }
}
