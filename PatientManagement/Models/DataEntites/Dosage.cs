using System;

namespace PatientManagement.Models.DataEntites
{
    public class Dosage
    {
        public string Id { get; set; }
        public string Dose { get; set; }
        public Dosage(string? id, string dose)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Dose = dose;
        }
    }
}
