using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ApplicationDbContext") 
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; } 
    }
}
