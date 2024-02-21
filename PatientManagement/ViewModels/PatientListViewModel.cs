using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public PatientListViewModel() { }
        public PatientListViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            _patients = new ObservableCollection<Patient>(PatientManager.getPatientsFromDb() ?? Enumerable.Empty<Patient>());
            ShowAddPatient = new RelayCommand(showAddPatientWindow);
            AddVisitCommand = new RelayCommand(AddVisit);

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
        private string? _selectedPatientId;

        public string? SelectedPatientId
        {
            get { return _selectedPatientId; }
            set
            {
                if (_selectedPatientId != value)
                {
                    _selectedPatientId = value;
                    OnPropertyChanged(nameof(SelectedPatientId));

                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddVisit(Object obj)
        {
            try
            {
                Visit? newVisit = VisitManager.AddVisitToPatient(SelectedPatientId);
                if (newVisit != null)
                {
                    _patientStore.CreateVisit(newVisit);
                }
            }
            catch (Exception ex) { }
        }

        private void showAddPatientWindow(object obj)
        {
            Console.WriteLine("show patient add window");
            AddPatient addPatientWin = new AddPatient();
            addPatientWin.ShowDialog();
        }

    }
}
