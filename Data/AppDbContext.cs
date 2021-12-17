using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;

namespace Data
{
   public class AppDbContext : IdentityDbContext<AppUser>
   {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<JobNature> JobNatures { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Resume> Resumes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Job>().Property(p => p.MinimumSalary).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Job>().Property(p => p.MaximumSalary).HasColumnType("decimal(18,4)");

        }


    }
}
