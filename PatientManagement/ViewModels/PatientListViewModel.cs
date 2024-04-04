using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.Views;
using PatientManagement.Views.ConfirmationWindows;
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
        public ICommand ShowAddPatientCommand { get; set; }
        public ICommand AddVisitCommand { get; }
        public ICommand DeletePatientCommand { get; }
        public ICommand EditPatientCommand { get; }

        private PatientStore _patientStore;

        public PatientListViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            _patients = new ObservableCollection<Patient>(PatientManager.getPatientsFromDb() ?? Enumerable.Empty<Patient>());
            ShowAddPatientCommand = new RelayCommand(showAddPatientWindow);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            EditPatientCommand = new RelayCommand(ShowEditpatientWindow);
            AddVisitCommand = new RelayCommand(AddVisit);
            _patientStore.PatientCreated += OnPatientCreated;
            _patientStore.PatientEdited += OnPatientEdited;
        }

        private void OnPatientEdited(Patient EditedPatient)
        {
            Patient? patient = Patients.FirstOrDefault(p => p.Id == EditedPatient.Id);
            if (patient != null)
            {
                Patients.Remove(patient);
                Patients.Add(EditedPatient);

            }
        }

        private void ShowEditpatientWindow(object obj)
        {
            if (obj is Patient patient)
            {
                EditPatientViewModel editPatientViewModel = new EditPatientViewModel(_patientStore, patient);
                EditPatientWindow editPatientWindow = new EditPatientWindow
                {
                    DataContext = editPatientViewModel
                };
                editPatientWindow.ShowDialog();

            }
        }

        private void DeletePatient(object obj)
        {

            if (obj is Patient patient)
            {
                var confirmationWindow = new DeleteConfirmationWindow("Deleting Patient:" + patient.Name);
                confirmationWindow.ShowDialog();

                if (confirmationWindow.Confirmed)
                {
                    PatientManager.DeletePatient(patient);
                    Patients.Remove(patient);
                }
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
                //Debug.WriteLine("selected EditedPatient:" + _selectedPatient);
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
                        HistoryManager.getHistoryTableForVisit(newVisit.Id);


                        //this is a bit of a shortcut as it will refetch all the visits from the database
                        //and also change the focus of the listView
                        _patientStore.ChangePatientSelection(patient);

                    }
                }

            }
            catch (Exception ex) { }
        }


    }
}
