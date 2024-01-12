using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Models.DataEntites
{
    public class PatientRecord
    {
        public string Id { get; }
        public PatientRecordHeading PatientRecordHeading { get; set; }
        public string PatientRecordDetail { get; set; }

    }
}
