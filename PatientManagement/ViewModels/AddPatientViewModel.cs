using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class AddPatientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        static string DefaultGenderValue { get; } = "Male";
        private bool isDuplicate = false;
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _age = "";
        public string Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private string _selectedGender = DefaultGenderValue;
        public string SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                if (_selectedGender != value)
                {
                    _selectedGender = value;
                    OnPropertyChanged(nameof(SelectedGender));
                }
            }
        }

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public ICommand AddPatientCommand { get; set; }

        private PatientStore _patientStore;

        public AddPatientViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            Debug.WriteLine("Running addPatientConstructor");
            AddPatientCommand = new RelayCommand(AddPatient, CanAddPatient);
        }

        private bool CanAddPatient()
        {
            ErrorMessage = "";
            Debug.WriteLine("running canAddPatient");
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Enter Name";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Age))
            {
                ErrorMessage = "Enter Age";
                return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedGender))
            {
                ErrorMessage = "Select a gender";
                return false;
            }
            if (isDuplicate)
            {
                ErrorMessage = "Cannot insert duplicate value (name,age,gender)";
            }
            return true;
        }

        internal void AddPatient(object? parameter)
        {
            isDuplicate = false;
            if (!CanAddPatient())
            {
                // Handle invalid input
                Debug.WriteLine("Invalid input. Cannot add patient.");
                return;
            }

            Debug.WriteLine($"{Name} {Age} {SelectedGender}");

            Patient newPatient = new Patient()
            {
                Name = Name.Trim(),
                Age = int.Parse(Age),
                Gender = SelectedGender.Trim(),
            };
            try
            {
                newPatient = PatientManager.addPatientToDb(newPatient);
                _patientStore.CreatePatient(newPatient);
            }
            catch (Exception ex)
            {
                isDuplicate = true;
                return;
            }

            // Reset input fields
            Name = "";
            Age = "";
            SelectedGender = DefaultGenderValue;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
