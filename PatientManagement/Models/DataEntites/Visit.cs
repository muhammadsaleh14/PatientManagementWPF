using System;
using System.Collections.ObjectModel;

namespace PatientManagement.Models.DataEntites
{
    public class Visit
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string? OptionalDetail { get; set; }
        public ObservableCollection<PatientHistory> PatientRecords { get; set; } = new ObservableCollection<PatientHistory>();
        public ObservableCollection<Prescription> Prescriptions { get; set; } = new ObservableCollection<Prescription>();
    }
}
