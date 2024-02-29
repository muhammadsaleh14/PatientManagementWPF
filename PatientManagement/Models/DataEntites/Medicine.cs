using System;

namespace PatientManagement.Models.DataEntites
{
    public class Medicine
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }


        public Medicine(string? id, string medicineName)
        {
            Id = id ?? Guid.NewGuid().ToString();
            MedicineName = medicineName;
        }
    }
}
