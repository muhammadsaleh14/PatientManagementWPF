using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        private static PatientViewModel? _instance;

        public static PatientViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PatientViewModel();
                }
                return _instance;
            }
        }
        private PatientViewModel()
        {
            Patients = new ObservableCollection<Patient>(PatientManager.getPatientsFromDb());
            ShowAddPatient = new RelayCommand(showAddPatientWindow);
            AddVisitCommand = new RelayCommand(AddVisit);

        }
        public ObservableCollection<Patient> Patients { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        public ICommand ShowAddPatient { get; set; }


        public ICommand AddVisitCommand { get; set; }



        private void AddVisit(Object obj)
        {
            VisitManager.AddVisitToPatient(SelectedPatientId);
        }

        private void showAddPatientWindow(object obj)
        {
            Console.WriteLine("show patient add window");
            AddPatient addPatientWin = new AddPatient();
            addPatientWin.ShowDialog();
        }

    }
}
