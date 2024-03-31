using System.Collections.Generic;

namespace PatientManagement.Stores
{
    public partial class PatientStore
    {
        public string? CurrentVisitId;
        public List<string>? AllDiagnosisDetails;
        public List<string>? AllHistoryHeadings;
        public List<string>? AllHistoryDetails;
    }
}
