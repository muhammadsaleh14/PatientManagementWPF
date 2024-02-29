using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class VisitListViewModel : INotifyPropertyChanged
    {
        private PatientStore _patientStore;
        public ICommand OpenVisitViewCommand;


        public VisitListViewModel(Stores.PatientStore patientStore)
        {
            _patientStore = patientStore;
            _patientStore.PatientSelectionChanged += OnPatientSelectionChanged;
            //_patientStore.VisitCreated += OnVisitCreated;
            _patientVisits = new ObservableCollection<Visit>(VisitManager.GetPatientVisitsFromDb(SelectedPatient?.Id) ?? Enumerable.Empty<Visit>());
            OpenVisitViewCommand = new RelayCommand(OpenVisitView);
        }

        private void OpenVisitView(object visitId)
        {
            _patientStore.CurrentVisitId = (string)visitId;
            VisitViewModel visitViewModel = new VisitViewModel(_patientStore);
            _patientStore.ChangeViewModel(visitViewModel);

        }

        //private void OnVisitCreated(VisitPage obj)
        //{
        //    if (obj is VisitPage visit)
        //    {
        //        PatientVisits.Add(visit);
        //    }

        //}

        private void OnPatientSelectionChanged(Patient? patient)
        {
            Debug.WriteLine("OnPatientSelectionChanged:" + patient.Name);
            IEnumerable<Visit> visits = VisitManager.GetPatientVisitsFromDb(patient?.Id) ?? Enumerable.Empty<Visit>();
            //foreach (VisitPage vis in visits)
            //{
            //    Debug.WriteLine("patient Visits:" + vis.Date + " detail:" + vis.OptionalDetail);
            //}
            SelectedPatient = patient;
            PatientVisits = new ObservableCollection<Visit>(visits);
            //OnPropertyChanged(nameof(PatientVisits));
        }

        private Patient? _selectedPatient;
        public Patient? SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
