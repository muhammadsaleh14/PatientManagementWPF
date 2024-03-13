using Microsoft.EntityFrameworkCore;
using PatientManagement.Models.DataEntites;
using System.Collections.Generic;

namespace PatientManagement.Models.Contexts
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Visit> Visits => Set<Visit>();


        public DbSet<HistoryTable> HistoryTables => Set<HistoryTable>();
        public DbSet<HistoryItem> HistoryItems => Set<HistoryItem>();
        public DbSet<HistoryHeading> HistoryHeadings => Set<HistoryHeading>();


        public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
        public DbSet<DiagnosisHeading> DiagnosisHeadings => Set<DiagnosisHeading>();



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
            modelBuilder.Entity<Patient>().Property(p => p.Name).HasColumnType("TEXT COLLATE NOCASE");//makes it case insensitive
            modelBuilder.Entity<Patient>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Patient>().HasIndex(p => new { p.Name, p.Gender, p.Age }).IsUnique();

            //Visit table
            modelBuilder.Entity<Visit>().Property(v => v.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Visit>().HasIndex(v => v.HistoryTableId);



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


            //Historytable table
            modelBuilder.Entity<HistoryTable>().Property(h => h.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<HistoryTable>().HasIndex(h => h.visit);

            //HistoryItem table
            modelBuilder.Entity<HistoryItem>().Property(h => h.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistoryItem>().HasIndex(h => h.HistoryTableId);

            //HistoryHeading Table
            modelBuilder.Entity<HistoryHeading>().Property(h => h.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HistoryHeading>().HasIndex(h => h.Heading).IsUnique();
            modelBuilder.Entity<HistoryHeading>().HasIndex(h => new { h.Heading, h.Priority }).IsUnique();
            modelBuilder.Entity<HistoryHeading>().Property(h => h.Heading).HasColumnType("TEXT COLLATE NOCASE");//makes it case insensitive

            //Diagnosis table
            modelBuilder.Entity<Diagnosis>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Diagnosis>().HasIndex(d => d.VisitId);


            //DiagnosisHeading table
            modelBuilder.Entity<DiagnosisHeading>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DiagnosisHeading>().HasIndex(d => d.Heading).IsUnique();
            modelBuilder.Entity<DiagnosisHeading>().HasIndex(d => d.Priority).IsUnique();
            modelBuilder.Entity<DiagnosisHeading>().Property(d => d.Heading).HasColumnType("TEXT COLLATE NOCASE");//makes it case insensitive
            modelBuilder.Entity<DiagnosisHeading>().HasIndex(d => d.IsActive);





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
