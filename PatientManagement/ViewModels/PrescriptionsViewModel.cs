using PatientManagement.Commands;
using PatientManagement.CustomComponents;
using PatientManagement.Models;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class PrescriptionsViewModel : INotifyPropertyChanged
    {
        public ICommand AddPrescriptionCommand { get; }
        public ICommand DeletePrescriptionCommand { get; }
        public ICommand EditPrescriptionCommand { get; }

        private string _visitId;


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


        private string _addMedicineText = "";
        public string AddMedicineText
        {
            get { return _addMedicineText; }
            set
            {
                Debug.WriteLine(value);
                if (_addMedicineText != value)
                {
                    _addMedicineText = value;
                    OnPropertyChanged(nameof(AddMedicineText));
                }
            }
        }

        private string _addDosageText = "";
        public string AddDosageText
        {
            get { return _addDosageText; }
            set
            {
                if (_addDosageText != value)
                {
                    _addDosageText = value;
                    OnPropertyChanged(nameof(AddDosageText));
                }
            }
        }

        private string _addDurationText = "";
        public string AddDurationText
        {
            get { return _addDurationText; }
            set
            {
                if (_addDurationText != value)
                {
                    _addDurationText = value;
                    OnPropertyChanged(nameof(AddDurationText));
                }
            }
        }

        private HashSet<string> _allMedicines;
        public HashSet<string> AllMedicines
        {
            get { return _allMedicines; }
            set
            {
                if (_allMedicines != value)
                {
                    _allMedicines = value;
                    OnPropertyChanged(nameof(AllMedicines));
                }
            }
        }


        private HashSet<string> _allDosages;
        public HashSet<string> AllDosages
        {
            get { return _allDosages; }
            set
            {
                if (_allDosages != value)
                {
                    _allDosages = value;
                    OnPropertyChanged(nameof(AllDosages));
                }
            }
        }

        private HashSet<string> _allDurations;

        public HashSet<string> AllDurations
        {
            get { return _allDurations; }
            set
            {
                if (_allDurations != value)
                {
                    _allDurations = value;
                    OnPropertyChanged(nameof(AllDurations));
                }
            }
        }

        private ObservableCollection<Prescription> _prescriptionsList;

        public ObservableCollection<Prescription> PrescriptionsList
        {
            get { return _prescriptionsList; }
            set
            {
                _prescriptionsList = value;
                OnPropertyChanged(nameof(PrescriptionsList));
            }
        }

        private PatientStore _patientStore;


        public PrescriptionsViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            _visitId = patientStore.CurrentVisitId ?? throw new Exception("Visit Id is null");
            _prescriptionsList = new ObservableCollection<Prescription>(PrescriptionManager.getPatientVisitPrescriptions(_visitId));
            //From PrescriptionManager Class
            _allMedicines = new HashSet<string>(PrescriptionManager.getAllMedicineNames());
            _allDosages = new HashSet<string>(PrescriptionManager.getAllDosageValues());
            _allDurations = new HashSet<string>(PrescriptionManager.getAllDurationTimes());

            AddPrescriptionCommand = new RelayCommand(AddPrescription, CanAddPrescription);
            DeletePrescriptionCommand = new RelayCommand(DeletePrescription);
            EditPrescriptionCommand = new RelayCommand(EditPrescription);
            //_allPrescriptionsValues = PrescriptionManager.GetAllPrescriptionsValues() ?? new List<string>();
        }

        private void EditPrescription(object obj)
        {
            try
            {
                if (obj is Prescription prescription)
                {
                    AddMedicineText = prescription.Medicine.MedicineName;
                    AddDosageText = prescription.Dosage.Dose;
                    AddDurationText = prescription.Duration.DurationTime;
                    DeletePrescription(prescription);
                }
            }
            catch (Exception ex)
            {
                Message.MessageText = "Error: " + ex.Message;
            }
        }

        private void DeletePrescription(object obj)
        {
            try
            {

                if (obj is Prescription prescription)
                {
                    PrescriptionManager.DeletePrescription(prescription);
                    PrescriptionsList.Remove(prescription);
                }
            }
            catch (Exception ex)
            {
                Message.MessageText = "Error: " + ex.Message;
            }

        }

        private bool CanAddPrescription()
        {
            if (string.IsNullOrWhiteSpace(AddMedicineText))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(AddDosageText))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(AddDurationText))
            {
                return false;
            }
            return true;
        }

        private void AddPrescription(object? param)
        {
            try
            {
                Prescription data = PrescriptionManager.AddPrescriptionToVisit(_visitId, AddMedicineText, AddDosageText, AddDurationText);
                PrescriptionsList.Add(data);

                Debug.WriteLine("Prescription is" + data.Dosage.Dose + "X" + data.Dosage.Id + "X" + data.Medicine.MedicineName);

                _allMedicines.Add(AddMedicineText);
                _allDosages.Add(AddDosageText);
                _allDurations.Add(AddDurationText);

                AddMedicineText = "";
                AddDosageText = "";
                AddDurationText = "";
                //Debug.WriteLine(data.Dosage);
                //Debug.WriteLine(data.Duration);

            }
            catch (Exception e)
            {
                Message.MessageText = "Error: " + e.Message;
            }

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
