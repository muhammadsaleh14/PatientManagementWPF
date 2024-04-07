using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using System;
using System.Windows.Input;

namespace PatientManagement.ViewModels.Items
{
    public class DiagnosisHeadingViewModel : ViewModelBase
    {
        //All Commands that change the heading/s or priotity will be utilised by the manager view
        private PatientStore _patientStore;

        public ICommand ToggleEnabledHeadingCommand { get; }
        public ICommand IncreasePriorityCommand { get; }

        private DiagnosisHeading _diagnosisHeading;

        public DiagnosisHeading DiagnosisHeading
        {
            get { return _diagnosisHeading; }
            set
            {
                _diagnosisHeading = value;
                OnPropertyChanged(nameof(DiagnosisHeading));
            }
        }

        private string _editHeadingText;

        public string EditHeadingText
        {
            get { return _editHeadingText; }
            set { _editHeadingText = value; }
        }




        public DiagnosisHeadingViewModel(PatientStore patientStore, DiagnosisHeading diagnosisHeading)
        {
            this._patientStore = patientStore;
            this._diagnosisHeading = diagnosisHeading;
            _editHeadingText = diagnosisHeading.Heading;

            ToggleEnabledHeadingCommand = new RelayCommand(ToggleEnabledHeading);
            IncreasePriorityCommand = new RelayCommand(IncreasePriority);
        }



        private void IncreasePriority(object obj)
        {
            try
            {
                DiagnosisManager.IncreasePriority(DiagnosisHeading);
                _patientStore.ChangeDiagnosisHeadingPriority();

            }
            catch (Exception ex)
            {
                _patientStore.DisplayErrorDiagnosisHeading("Error: " + ex.Message);
            }
        }


        private void ToggleEnabledHeading(object obj)
        {
            try
            {
                if (DiagnosisHeading.IsActive == true)
                {
                    DiagnosisHeading = DiagnosisManager.DisableHeading(DiagnosisHeading);
                }
                else
                {
                    DiagnosisHeading = DiagnosisManager.EnableHeading(DiagnosisHeading);
                }

            }
            catch (Exception ex)
            {
                _patientStore.DisplayErrorDiagnosisHeading("Error: " + ex.Message);
            }

        }
    }
}
