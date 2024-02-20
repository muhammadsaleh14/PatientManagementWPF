using Microsoft.EntityFrameworkCore;
using PatientManagement.Models.DataEntites;

namespace PatientManagement.Models.Contexts
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Visit>? Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={ModelConfigurations.DBpath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //patient Table
            modelBuilder.Entity<Patient>().Property(p => p.Name).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Patient>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Patient>().HasIndex(p => new { p.Name, p.Gender, p.Age }).IsUnique();

            //Visit table
            modelBuilder.Entity<Visit>().Property(v => v.Id).ValueGeneratedOnAdd();
        }
    }
}
