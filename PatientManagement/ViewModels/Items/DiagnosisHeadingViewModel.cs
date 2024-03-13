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

        public ICommand DeleteHeadingCommand;
        public ICommand EditHeadingCommand;
        public ICommand ChangePriorityCommand;

        private DiagnosisHeading _diagnosisHeading;

        public DiagnosisHeading DiagnosisHeading
        {
            get { return _diagnosisHeading; }
            set
            {
                if (_diagnosisHeading.Heading != value.Heading)
                {
                    _diagnosisHeading.Heading = value.Heading;
                    OnPropertyChanged(nameof(DiagnosisHeading));

                }
            }
        }


        public DiagnosisHeadingViewModel(PatientStore patientStore, DiagnosisHeading diagnosisHeading)
        {
            this._patientStore = patientStore;
            this._diagnosisHeading = diagnosisHeading;
            DeleteHeadingCommand = new RelayCommand(DeleteHeading);
            EditHeadingCommand = new RelayCommand(EditHeading);
            ChangePriorityCommand = new RelayCommand(ChangePriority);
        }

        private void ChangePriority(object obj)
        {
            throw new NotImplementedException();
        }


        private void EditHeading(object obj)
        {
            if (obj is DiagnosisHeadingViewModel diagnosisHeadingViewModel)
            {
                DiagnosisManager.EditDiagnosisHeading(diagnosisHeadingViewModel._diagnosisHeading);
            }
        }

        private void DeleteHeading(object obj)
        {

        }
    }
}
