using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models.DataEntites
{
    public class Diagnosis
    {

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Visit))]
        public string VisitId { get; set; }
        public Visit Visit { get; set; } = null!;

        [ForeignKey(nameof(DiagnosisHeading))]
        public string DiagnosisHeadingId { get; set; }
        public DiagnosisHeading DiagnosisHeading { get; set; } = null!;


        public string Detail { get; set; }
        public Diagnosis(string? id, string detail, string visitId, string diagnosisHeadingId)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Detail = detail;
            VisitId = visitId;
            DiagnosisHeadingId = diagnosisHeadingId;
        }

    }
}
