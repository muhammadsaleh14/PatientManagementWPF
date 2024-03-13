using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using System.Windows.Input;

namespace PatientManagement.ViewModels.Managers
{
    public class VisitViewModel : ViewModelBase
    {
        public PrescriptionsViewModel PrescriptionsViewModel { get; }
        public HistoryTableViewModel HistoriesViewModel { get; set; }

        public DiagnosisViewModel DiagnosisViewModel { get; set; }

        public ICommand OpenPatientsViewCommand { get; }

        private PatientStore _patientStore;

        private Visit _visit;

        public Visit Visit
        {
            get { return _visit; }
            set { _visit = value; }
        }


        public VisitViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            PrescriptionsViewModel = new PrescriptionsViewModel(_patientStore);
            HistoriesViewModel = new HistoryTableViewModel(_patientStore);
            DiagnosisViewModel = new DiagnosisViewModel(_patientStore, false);
            _visit = VisitManager.getVisitDetails(_patientStore.CurrentVisitId);
            OpenPatientsViewCommand = new RelayCommand(OpenPatientsView);
        }

        private void OpenPatientsView(object obj)
        {
            PatientsViewModel patientsViewModel = new PatientsViewModel(_patientStore);
            _patientStore.ChangeViewModel(patientsViewModel);
        }
    }
}
