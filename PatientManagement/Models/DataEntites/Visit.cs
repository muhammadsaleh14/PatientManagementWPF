using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Models.DataEntites
{
    public class Visit
    {
        public string Id { get;}
        public DateTime Date { get; set; }
        public ObservableCollection<PatientRecord> PatientRecords { get; set; } = new ObservableCollection<PatientRecord>();
        public ObservableCollection<Prescription> Prescriptions { get; set; } = new ObservableCollection<Prescription>();
    }
}
