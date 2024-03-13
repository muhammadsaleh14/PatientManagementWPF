using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models.DataEntites
{
    public class DiagnosisHeading
    {
        [Key]
        public string Id { get; set; }
        public string Heading { get; set; }

        public int Priority { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
        public DiagnosisHeading(string? id, string heading, int priority)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Heading = heading;
            Priority = priority;
            IsActive = true;
        }
    }
}
