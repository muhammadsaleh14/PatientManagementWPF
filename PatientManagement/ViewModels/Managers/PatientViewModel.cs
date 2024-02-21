namespace PatientManagement.ViewModels.Managers
{
    public class PatientViewModel
    {
        public AddPatientViewModel AddPatientViewModel { get; }
        public PatientListViewModel PatientListViewModel { get; }

        public PatientViewModel(AddPatientViewModel addPatientViewModel, PatientListViewModel patientListViewModel)
        {
            AddPatientViewModel = addPatientViewModel;
            PatientListViewModel = patientListViewModel;

        }
    }
}
