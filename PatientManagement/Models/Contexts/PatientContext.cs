using Microsoft.EntityFrameworkCore;
using PatientManagement.Models.DataEntites;
using System.Collections.Generic;

namespace PatientManagement.Models.Contexts
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Visit> Visits => Set<Visit>();


        public DbSet<History> Histories => Set<History>();
        public DbSet<HistoryHeading> HistoryHeadings => Set<HistoryHeading>();
        public DbSet<HistoryDetail> HistoryDetails => Set<HistoryDetail>();


        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Medicine> Medicines => Set<Medicine>();
        public DbSet<Dosage> Dosages => Set<Dosage>();
        public DbSet<Duration> Durations => Set<Duration>();


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

            //VisitPage table
            modelBuilder.Entity<Visit>().Property(v => v.Id).ValueGeneratedOnAdd();



            //Prescriptions table
            modelBuilder.Entity<Prescription>().HasKey(p => new { p.MedicineId, p.DosageId, p.DurationId });

            //Medicine Table
            modelBuilder.Entity<Medicine>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Medicine>().HasIndex(m => m.MedicineName).IsUnique();

            //Dosage Table
            modelBuilder.Entity<Dosage>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Dosage>().HasIndex(d => d.Dose).IsUnique();

            //Duration Table
            modelBuilder.Entity<Duration>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Duration>().HasIndex(d => d.DurationTime).IsUnique();



            //Histories table
            modelBuilder.Entity<History>().HasKey(h => new { h.HistoryHeadingId, h.HistoryDetailId });

            ////HistoryHeading Table
            modelBuilder.Entity<HistoryHeading>().Property(h => h.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistoryHeading>().HasIndex(h => h.Heading).IsUnique();
            modelBuilder.Entity<HistoryHeading>().HasIndex(h => h.Priority).IsUnique();


            ////HistoryDetail Table
            modelBuilder.Entity<HistoryDetail>().Property(h => h.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistoryDetail>().HasIndex(h => h.Detail).IsUnique();


            //VisitPage and history
            modelBuilder.Entity<Visit>()
                .HasMany(v => v.Histories)
                .WithMany(h => h.Visits)
                .UsingEntity<Dictionary<string, object>>(
                "VisitHistory",
                j => j.HasOne<History>().WithMany().HasForeignKey("HistoryHeadingId", "HistoryDetailId"),
                j => j.HasOne<Visit>().WithMany().HasForeignKey("VisitId"));


            //VisitPage and Prescription
            modelBuilder.Entity<Visit>()
                .HasMany(v => v.Prescriptions)
                .WithMany(p => p.Visits)
                .UsingEntity<Dictionary<string, object>>(
                "VisitPrescription",
                j => j.HasOne<Prescription>().WithMany().HasForeignKey("MedicineId", "DosageId", "DurationId"),
                j => j.HasOne<Visit>().WithMany().HasForeignKey("VisitId"));


        }
    }
}
