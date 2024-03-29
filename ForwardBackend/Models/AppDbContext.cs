﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ForwardBackend.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }

        public DataContext(DbContextOptions<DataContext> options) 
            : base(options) {
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            if (modelBuilder is null) {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<WorkExperience>().HasOne(w => w.Job).WithMany(j => j.WorkExperiences).HasForeignKey(w => w.JobForeignKey);
        }
    }
}
