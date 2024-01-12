using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Models.DataEntites
{
    public class Patient
    {

        public string? Id { get; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public DateOnly? DateCreated { get; }
        public ObservableCollection<Visit> Visits { get; set; } = new ObservableCollection<Visit>();
    }
}
