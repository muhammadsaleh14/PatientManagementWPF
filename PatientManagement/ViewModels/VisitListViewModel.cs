using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PatientManagement.ViewModels
{
    internal class VisitListViewModel : INotifyPropertyChanged
    {
        private string _selectedPatientId;
        public string SelectedPatientId
        {
            get { return _selectedPatientId; }
            set
            {
                _selectedPatientId = value;
                OnPropertyChanged(nameof(PatientVisits));
            }
        }

        private ObservableCollection<Visit> _patientVisits;
        public ObservableCollection<Visit> PatientVisits
        {
            get { return _patientVisits; }
            set
            {
                _patientVisits = value;
                OnPropertyChanged(nameof(PatientVisits));
            }
        }
        public VisitListViewModel()
        {
            _patientVisits = new ObservableCollection<Visit>(VisitManager.getVisitsFromDb(SelectedPatientId) ?? Enumerable.Empty<Visit>());

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
