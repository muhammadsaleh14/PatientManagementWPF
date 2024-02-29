using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;

namespace PatientManagement.ViewModels.Managers
{
    public class VisitViewModel : ViewModelBase
    {
        public PrescriptionsViewModel PrescriptionsViewModel { get; }


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
            _visit = VisitManager.getVisitDetails(_patientStore.CurrentVisitId);
        }
    }
}
