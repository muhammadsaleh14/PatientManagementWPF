using PatientManagement.Stores;
using PatientManagement.Views;
using System;

namespace PatientManagement.ViewModels.Managers
{
    public class PatientViewModel
    {
        public AddPatientViewModel AddPatientViewModel { get; }
        public PatientListViewModel PatientListViewModel { get; }
        public VisitListViewModel VisitListViewModel { get; }



        public PatientViewModel()
        {
            PatientStore patientStore = new PatientStore();

            VisitListViewModel = new VisitListViewModel(patientStore);
            AddPatientViewModel = new AddPatientViewModel(patientStore);
            PatientListViewModel = new PatientListViewModel(patientStore);

            patientStore.AddPatientWindowOpened += openAddPatientWindow;
        }

        private void openAddPatientWindow(PatientStore patientStore)
        {
            Console.WriteLine("show patient add window");
            AddPatient addPatientWin = new AddPatient();
            addPatientWin.DataContext = AddPatientViewModel;
            addPatientWin.ShowDialog();
        }
    }
}
