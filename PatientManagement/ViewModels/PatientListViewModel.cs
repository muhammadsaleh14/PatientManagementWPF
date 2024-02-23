using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class PatientListViewModel : INotifyPropertyChanged
    {
        //SINGLETON APPLICATION
        //private static PatientListViewModel? _instance;
        //public static PatientListViewModel Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new PatientListViewModel();
        //        }
        //        return _instance;
        //    }
        //}
        public ICommand ShowAddPatient { get; set; }
        public ICommand AddVisitCommand { get; set; }

        private PatientStore _patientStore;

        public PatientListViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            _patients = new ObservableCollection<Patient>(PatientManager.getPatientsFromDb() ?? Enumerable.Empty<Patient>());
            ShowAddPatient = new RelayCommand(showAddPatientWindow);
            AddVisitCommand = new RelayCommand(AddVisit);
            _patientStore.PatientCreated += OnPatientCreated;
        }

        private string _lastVisit;

        public string LastVisit
        {
            get { return _lastVisit; }
            set
            {
                _lastVisit = value;
                OnPropertyChanged(nameof(LastVisit));
            }
        }


        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }
        private Patient? _selectedPatient;

        public Patient? SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                //Debug.WriteLine("selected patient:" + _selectedPatient);
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    OnPropertyChanged(nameof(SelectedPatient));
                    Debug.WriteLine("selection changed Id:" + _selectedPatient);
                    _patientStore.ChangePatientSelection(_selectedPatient);

                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void showAddPatientWindow(object obj)
        {
            _patientStore.AddPatientWindowOpen(_patientStore);
        }

        private void OnPatientCreated(Patient newPatient)
        {
            _patients.Add(newPatient);
        }
        private void AddVisit(Object obj)
        {
            try
            {
                if (obj is Patient patient)
                {
                    Visit? newVisit = VisitManager.AddVisitToPatient(patient.Id);
                    if (newVisit != null)
                    {
                        _patientStore.ChangePatientSelection(patient);
                    }
                }

            }
            catch (Exception ex) { }
        }


    }
}
