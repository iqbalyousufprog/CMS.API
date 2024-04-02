using CMS.API.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace CMS.API.Data
{
    public class CMSDbContext : DbContext
    {
        public CMSDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disease>().HasData(
                    new Disease { Id = 1, DiseaseName = "Flu" },
                    new Disease { Id = 2, DiseaseName = "Cold" },
                    new Disease { Id = 3, DiseaseName = "Fever" },
                    new Disease { Id = 4, DiseaseName = "Headache" },
                    new Disease { Id = 5, DiseaseName = "Sore throat" },
                    new Disease { Id = 6, DiseaseName = "Stomach flu" },
                    new Disease { Id = 7, DiseaseName = "Pneumonia" },
                    new Disease { Id = 8, DiseaseName = "Sinusitis" },
                    new Disease { Id = 9, DiseaseName = "Bronchitis" },
                    new Disease { Id = 10, DiseaseName = "Allergies" }
            );
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
    }
}
