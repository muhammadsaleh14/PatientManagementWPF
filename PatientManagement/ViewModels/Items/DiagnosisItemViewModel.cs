using PatientManagement.AutoSaves;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using System;
using System.Diagnostics;

namespace PatientManagement.ViewModels.Items
{
    public class DiagnosisItemViewModel : ViewModelBase
    {
        private PatientStore _patientStore;

        private Diagnosis _diagnosis;

        public Diagnosis Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
                _heading = _diagnosis.DiagnosisHeading.Heading;
                _detail = _diagnosis.Detail;
            }
        }



        private string _heading;

        public string Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                OnPropertyChanged(nameof(Heading));
            }
        }

        private string _detail;
        public string Detail
        {
            get { return _detail; }
            set
            {
                _detail = value;
                OnPropertyChanged(nameof(Detail));
                AutoSaveVisit.UpdateText(nameof(Detail), _detail);
            }
        }


        public AutoSaveVisit AutoSaveVisit { get; set; }

        public DiagnosisItemViewModel(PatientStore patientStore, Diagnosis diagnosis)
        {
            _patientStore = patientStore;
            AutoSaveVisit = new AutoSaveVisit(_patientStore, saveDiagnosisDetailAction);
            _diagnosis = diagnosis;
            _heading = diagnosis.DiagnosisHeading.Heading;
            _detail = diagnosis.Detail;
        }



        private void saveDiagnosisDetailAction(string propertyName, string? textValue)
        {
            Debug.WriteLine("Running save on:" + propertyName + " value is:" + textValue);
            if (textValue == null)
            {
                throw new Exception("Parameter textValue is null in save action for " + propertyName);
            }
            if (propertyName == nameof(Detail))
            {
                Diagnosis = DiagnosisManager.EditDiagnosisDetail(Diagnosis.Id, textValue);
            }
        }

    }
}
