using PatientManagement.Stores;

namespace PatientManagement.ViewModels.Managers
{
    public class MainViewModel : ViewModelBase
    {
        private PatientStore _patientStore;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }


        public MainViewModel()
        {
            _patientStore = new PatientStore();
            //DiagnosisManager.sortPriorities();

            _patientStore.ViewModelChanged += OnCurrentViewModelChanged;
            _currentViewModel = new PatientsViewModel(_patientStore);
        }

        private void OnCurrentViewModelChanged(ViewModelBase newCurrentViewModel)
        {
            if (_currentViewModel is VisitViewModel visitViewModel)
            {
                if (visitViewModel.CanCloseVisitInt != 0)
                {
                    return;
                }

            }
            CurrentViewModel = newCurrentViewModel;
        }

    }
}
