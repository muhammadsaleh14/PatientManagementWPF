using PatientManagement.Models.DataEntites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models
{
    public class Prescription
    {
        //inside visits
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();


        //Has these properties
        public Medicine Medicine { get; set; } = null!;
        [ForeignKey(nameof(Medicine))]
        public string MedicineId { get; set; }

        public Dosage Dosage { get; set; } = null!;
        [ForeignKey(nameof(Dosage))]
        public string DosageId { get; set; }

        public Duration Duration { get; set; } = null!;
        [ForeignKey(nameof(Duration))]
        public string DurationId { get; set; }

        public Prescription(string medicineId, string dosageId, string durationId)
        {
            MedicineId = medicineId;
            DosageId = dosageId;
            DurationId = durationId;
        }

    }

}
