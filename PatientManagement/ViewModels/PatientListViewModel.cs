using PatientManagement.Commands;
using PatientManagement.CustomComponents;
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
using System.Threading.Tasks;
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
            _searchPatientText = string.Empty;
            _patientStore = patientStore;
            _patients = new ObservableCollection<Patient>(PatientManager.getPatientsFromDb() ?? Enumerable.Empty<Patient>());
            _filteredpatients = _patients;
            ShowAddPatientCommand = new RelayCommand(showAddPatientWindow);
            DeletePatientCommand = new RelayCommand(DeletePatient);
            EditPatientCommand = new RelayCommand(ShowEditpatientWindow);
            AddVisitCommand = new RelayCommand(AddVisit);
            _patientStore.PatientCreated += OnPatientCreated;
            _patientStore.PatientEdited += OnPatientEdited;
        }

        private async void SearchListForPatientAsync()
        {
            var result = new ObservableCollection<Patient>();

            result = await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(SearchPatientText))
                {
                    result = Patients;
                    return result;
                }

                string[] searchParams = SearchPatientText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string name = string.Empty;
                int age = 0;

                foreach (var param in searchParams)
                {
                    if (int.TryParse(param, out int parsedAge))
                    {
                        age = parsedAge;
                    }
                    else
                    {
                        name += param + " ";
                    }
                }

                name = name.Trim();

                // Perform search based on name and age
                return result = new ObservableCollection<Patient>(Patients.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) && (age == 0 || (age > p.Age - 5 && age < p.Age + 5))));
            });

            FilteredPatients = result;
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
            try
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
            catch (Exception ex)
            {
                Message.MessageText = "Error: " + ex.Message;
            }

        }

        private ObservableCollection<Patient> _filteredpatients;
        public ObservableCollection<Patient> FilteredPatients
        {
            get { return _filteredpatients; }
            set
            {
                _filteredpatients = value;
                OnPropertyChanged(nameof(FilteredPatients));
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
                FilteredPatients = _patients;
                SearchListForPatientAsync();

            }
        }

        private Message _message = new Message();

        public Message Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        private string _searchPatientText;

        public string SearchPatientText
        {
            get { return _searchPatientText; }
            set
            {
                _searchPatientText = value;
                OnPropertyChanged(nameof(SearchPatientText));
                SearchListForPatientAsync();
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
            catch (Exception ex)
            {
                Message.MessageText = "Error: " + ex.Message;
            }
        }


    }
}
