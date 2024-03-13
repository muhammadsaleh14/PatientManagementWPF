using PatientManagement.Stores;

namespace PatientManagement.ViewModels
{
    public class ManagerViewModel
    {
        public DiagnosisViewModel DiagnosisViewModel { get; }

        public ManagerViewModel(PatientStore patientStore)
        {
            DiagnosisViewModel = new DiagnosisViewModel(patientStore, true);
        }
    }
}
