﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models.DataEntites
{
    public class Visit
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Patient))]
        public string PatientId { get; set; }

        [Required]
        public Patient Patient { get; set; } = null!;

        [ForeignKey(nameof(HistoryTable))]
        public string? HistoryTableId { get; set; }

        public HistoryTable? HistoryTable { get; set; } = null!;


        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        public Visit(string? id, string patientId, string? optionalDetail, DateTime date)
        {
            Id = id ?? Guid.NewGuid().ToString();
            PatientId = patientId;
            Date = date;
            OptionalDetail = optionalDetail;
        }

        public DateTime Date { get; set; }
        public string? OptionalDetail { get; set; }

    }
}
