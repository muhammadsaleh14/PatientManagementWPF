using PatientManagement.Commands;
using PatientManagement.Stores;
using PatientManagement.Views;
using System.Windows.Input;

namespace PatientManagement.ViewModels.Managers
{
    public class PatientsViewModel : ViewModelBase
    {

        private PatientStore _patientStore { get; }
        //public AddPatientViewModel AddPatientViewModel { get; }
        public PatientListViewModel PatientListViewModel { get; }
        public VisitListViewModel VisitListViewModel { get; }

        public ICommand ShowManagerCommand { get; }

        public PatientsViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            VisitListViewModel = new VisitListViewModel(patientStore);
            //AddPatientViewModel = new AddPatientViewModel(_patientStore);
            PatientListViewModel = new PatientListViewModel(patientStore);


            ShowManagerCommand = new RelayCommand(ShowManagerWindow);
            patientStore.AddPatientWindowOpened += ShowAddPatientWindow;
        }

        private void ShowManagerWindow(object obj)
        {
            ManagerWindow managerWindow = new ManagerWindow();
            managerWindow.DataContext = new ManagerViewModel(_patientStore);
            managerWindow.ShowDialog();
        }

        private void ShowAddPatientWindow(PatientStore patientStore)
        {

            AddPatient addPatientWin = new AddPatient();
            AddPatientViewModel addPatientViewModel = new AddPatientViewModel(patientStore);
            addPatientWin.DataContext = addPatientViewModel;
            patientStore.AddPatientWindowOpened -= ShowAddPatientWindow;
            addPatientWin.ShowDialog();
            patientStore.AddPatientWindowOpened += ShowAddPatientWindow;

        }
    }
}
