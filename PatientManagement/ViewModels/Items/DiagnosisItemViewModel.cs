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

        private Diagnosis _diagnosisItem;

        public Diagnosis DiagnosisItem
        {
            get { return _diagnosisItem; }
            set
            {
                if (_diagnosisItem.Detail != value.Detail)
                {
                    _diagnosisItem.Detail = value.Detail;
                    OnPropertyChanged(nameof(DiagnosisItem.Detail));
                    AutoSaveTimer.UpdateText(nameof(DiagnosisItem.Detail), _diagnosisItem.Detail);
                }
            }
        }

        private readonly AutoSaveTimer AutoSaveTimer;

        public DiagnosisItemViewModel(PatientStore patientStore, Diagnosis diagnosis)
        {
            _patientStore = patientStore;
            AutoSaveTimer = new AutoSaveTimer(saveAction);
            _diagnosisItem = diagnosis;
        }

        private void saveAction(string propertyName, string? textValue)
        {
            Debug.WriteLine("Running save on:" + propertyName + " value is:" + textValue);
            if (textValue == null)
            {
                throw new Exception("Parameter textValue is null for Diagnosis save action");
            }
            if (propertyName == nameof(DiagnosisItem.Detail))
            {
                DiagnosisItem = DiagnosisManager.EditDiagnosisDetail(DiagnosisItem.Id, textValue);
            }
        }

    }
}
