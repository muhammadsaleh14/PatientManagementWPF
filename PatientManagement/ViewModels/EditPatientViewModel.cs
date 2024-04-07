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
    class EditPatientViewModel
    {

        private bool isDuplicate = false;
        private string _name;
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

        private string _age;
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

        private string _selectedGender;
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

        public ICommand EditPatientCommand { get; set; }

        private PatientStore _patientStore;
        private string _patientId;

        public EditPatientViewModel(PatientStore patientStore, Patient patient)
        {
            _patientStore = patientStore;
            _patientId = patient.Id;
            _name = patient.Name;
            _age = patient.Age.ToString();
            _selectedGender = patient.Gender;
            //Debug.WriteLine("Running addPatientConstructor");
            EditPatientCommand = new RelayCommand(EditPatient, CanEditPatient);
        }

        private bool CanEditPatient()
        {
            ErrorMessage = "";
            //Debug.WriteLine("running canAddPatient");
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

        internal void EditPatient(object? parameter)
        {
            isDuplicate = false;
            if (!CanEditPatient())
            {
                // Handle invalid input
                Debug.WriteLine("Invalid input. Cannot add patient.");
                return;
            }

            Debug.WriteLine($"{Name} {Age} {SelectedGender}");

            Patient newPatient = new Patient(id: _patientId, name: Name.Trim(), age: int.Parse(Age), gender: SelectedGender.Trim());

            try
            {
                newPatient = PatientManager.EditPatient(newPatient);
                _patientStore.EditPatient(newPatient);

            }
            catch (Exception ex)
            {
                isDuplicate = true;
                ErrorMessage = "Error: " + ex.Message;
                return;
            }


        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
