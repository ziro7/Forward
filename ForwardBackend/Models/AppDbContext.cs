using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.EntityFrameworkCore;

namespace ForwardBackend.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) {
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<WorkExperience>().HasOne(w => w.Job).WithMany(j => j.WorkExperiences).HasForeignKey(w => w.JobForeignKey);
        }
    }
}
