using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace PatientManagement.ViewModels
{
    public class VisitListViewModel : INotifyPropertyChanged
    {
        private PatientStore _patientStore;


        public VisitListViewModel(Stores.PatientStore patientStore)
        {
            _patientStore = patientStore;
            _patientStore.PatientSelectionChanged += OnPatientSelectionChanged;
            //_patientStore.VisitCreated += OnVisitCreated;
            _patientVisits = new ObservableCollection<Visit>(VisitManager.getVisitsFromDb(SelectedPatient?.Id) ?? Enumerable.Empty<Visit>());

        }

        //private void OnVisitCreated(Visit obj)
        //{
        //    if (obj is Visit visit)
        //    {
        //        PatientVisits.Add(visit);
        //    }

        //}

        private void OnPatientSelectionChanged(Patient? patient)
        {
            List<Visit> visits = VisitManager.getPatientsVisitFromDb(patient?.Id);
            foreach (Visit vis in visits)
            {
                Debug.WriteLine("patient Visits:" + vis.Date + " detail:" + vis.OptionalDetail);
            }
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
