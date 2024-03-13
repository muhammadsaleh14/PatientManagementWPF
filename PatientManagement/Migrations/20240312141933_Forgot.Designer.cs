﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientManagement.Models.Contexts;

#nullable disable

namespace PatientManagement.Migrations
{
    [DbContext(typeof(PatientContext))]
    [Migration("20240312141933_Forgot")]
    partial class Forgot
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Diagnosis", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DiagnosisHeadingId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("VisitId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DiagnosisHeadingId");

                    b.HasIndex("VisitId");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.DiagnosisHeading", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Heading")
                        .IsUnique();

                    b.HasIndex("IsActive");

                    b.HasIndex("Heading", "Priority")
                        .IsUnique();

                    b.ToTable("DiagnosisHeadings");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Dosage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Dose")
                        .IsUnique();

                    b.ToTable("Dosages");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Duration", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DurationTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DurationTime")
                        .IsUnique();

                    b.ToTable("Durations");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.HistoryHeading", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Heading")
                        .IsUnique();

                    b.HasIndex("Heading", "Priority")
                        .IsUnique();

                    b.ToTable("HistoryHeadings");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.HistoryItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HistoryHeadingId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HistoryTableId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HistoryHeadingId");

                    b.HasIndex("HistoryTableId");

                    b.ToTable("HistoryItems");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.HistoryTable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("InitialVisitId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoryTables");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Medicine", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("MedicineName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MedicineName")
                        .IsUnique();

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Patient", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Gender", "Age")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Visit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("HistoryTableId")
                        .HasColumnType("TEXT");

                    b.Property<string>("OptionalDetail")
                        .HasColumnType("TEXT");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HistoryTableId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("PatientManagement.Models.Prescription", b =>
                {
                    b.Property<string>("MedicineId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DosageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DurationId")
                        .HasColumnType("TEXT");

                    b.HasKey("MedicineId", "DosageId", "DurationId");

                    b.HasIndex("DosageId");

                    b.HasIndex("DurationId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("VisitPrescription", b =>
                {
                    b.Property<string>("VisitId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MedicineId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DosageId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DurationId")
                        .HasColumnType("TEXT");

                    b.HasKey("VisitId", "MedicineId", "DosageId", "DurationId");

                    b.HasIndex("MedicineId", "DosageId", "DurationId");

                    b.ToTable("VisitPrescription");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Diagnosis", b =>
                {
                    b.HasOne("PatientManagement.Models.DataEntites.DiagnosisHeading", "DiagnosisHeading")
                        .WithMany("Diagnoses")
                        .HasForeignKey("DiagnosisHeadingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagement.Models.DataEntites.Visit", "Visit")
                        .WithMany("Diagnoses")
                        .HasForeignKey("VisitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiagnosisHeading");

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.HistoryItem", b =>
                {
                    b.HasOne("PatientManagement.Models.DataEntites.HistoryHeading", "HistoryHeading")
                        .WithMany("HistoryItems")
                        .HasForeignKey("HistoryHeadingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagement.Models.DataEntites.HistoryTable", "HistoryTable")
                        .WithMany("HistoryItems")
                        .HasForeignKey("HistoryTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoryHeading");

                    b.Navigation("HistoryTable");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Visit", b =>
                {
                    b.HasOne("PatientManagement.Models.DataEntites.HistoryTable", "HistoryTable")
                        .WithMany("Visits")
                        .HasForeignKey("HistoryTableId");

                    b.HasOne("PatientManagement.Models.DataEntites.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoryTable");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PatientManagement.Models.Prescription", b =>
                {
                    b.HasOne("PatientManagement.Models.DataEntites.Dosage", "Dosage")
                        .WithMany()
                        .HasForeignKey("DosageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagement.Models.DataEntites.Duration", "Duration")
                        .WithMany()
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagement.Models.DataEntites.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dosage");

                    b.Navigation("Duration");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("VisitPrescription", b =>
                {
                    b.HasOne("PatientManagement.Models.DataEntites.Visit", null)
                        .WithMany()
                        .HasForeignKey("VisitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PatientManagement.Models.Prescription", null)
                        .WithMany()
                        .HasForeignKey("MedicineId", "DosageId", "DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.DiagnosisHeading", b =>
                {
                    b.Navigation("Diagnoses");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.HistoryHeading", b =>
                {
                    b.Navigation("HistoryItems");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.HistoryTable", b =>
                {
                    b.Navigation("HistoryItems");

                    b.Navigation("Visits");
                });

            modelBuilder.Entity("PatientManagement.Models.DataEntites.Visit", b =>
                {
                    b.Navigation("Diagnoses");
                });
#pragma warning restore 612, 618
        }
    }
}