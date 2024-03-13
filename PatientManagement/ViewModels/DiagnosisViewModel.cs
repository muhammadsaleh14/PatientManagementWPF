using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Items;
using PatientManagement.ViewModels.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class DiagnosisViewModel : ViewModelBase
    {
        private PatientStore _patientStore;

        public ICommand AddDiagnosisHeadingCommand { get; }

        private string _errorMessage = string.Empty;
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

        private string _addDiagnosisHeadingText;

        public string AddDiagnosisHeadingText
        {
            get { return _addDiagnosisHeadingText; }
            set
            {
                _addDiagnosisHeadingText = value;
                OnPropertyChanged(nameof(AddDiagnosisHeadingText));
            }
        }


        private ObservableCollection<DiagnosisHeadingViewModel> _activeDiagnosisHeadings;
        public ObservableCollection<DiagnosisHeadingViewModel> ActiveDiagnosisHeadings
        {
            get { return _activeDiagnosisHeadings; }
            set { _activeDiagnosisHeadings = value; }
        }


        private ObservableCollection<DiagnosisItemViewModel>? _diagnosisItems;
        public ObservableCollection<DiagnosisItemViewModel>? DiagnosisItems
        {
            get { return _diagnosisItems; }
            set
            {
                _diagnosisItems = value;
            }
        }


        public DiagnosisViewModel(PatientStore patientStore, bool isConfigWindow)
        {
            _patientStore = patientStore;
            _addDiagnosisHeadingText = string.Empty;
            _activeDiagnosisHeadings = new ObservableCollection<DiagnosisHeadingViewModel>();
            AddDiagnosisHeadingCommand = new RelayCommand(AddDiagnosisHeading, CanAddDiagnosisHeading);
            if (!isConfigWindow)
            {
                string currentVisitId = _patientStore.CurrentVisitId ?? throw new Exception("Visitid is not availible for diagnosis");
                _diagnosisItems = DiagnosisItemViewModelProjection(DiagnosisManager.getDiagnosisForVisit(currentVisitId));

            }
            else
            {
                _activeDiagnosisHeadings = DiagnosisHeadingViewModelProjection(DiagnosisManager.getDiagnosisHeadings());

            }
        }

        private bool CanAddDiagnosisHeading()
        {
            if (AddDiagnosisHeadingText != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void AddDiagnosisHeading(object obj)
        {
            try
            {
                DiagnosisHeading diagnosisHeading = DiagnosisManager.AddDiagnosisHeading(AddDiagnosisHeadingText);
                ActiveDiagnosisHeadings.Add(new DiagnosisHeadingViewModel(_patientStore, diagnosisHeading));
                AddDiagnosisHeadingText = string.Empty;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                await Task.Delay(1000);
                ErrorMessage = string.Empty;
                Debug.WriteLine(ex.StackTrace);
            }
            //DiagnosisManager.AddDiagnosisHeadingText();
        }

        private ObservableCollection<DiagnosisHeadingViewModel> DiagnosisHeadingViewModelProjection(List<DiagnosisHeading> diagnosisHeadings)
        {
            ObservableCollection<DiagnosisHeadingViewModel>? result = new ObservableCollection<DiagnosisHeadingViewModel>();
            foreach (var diagnosisHeading in diagnosisHeadings)
            {
                result.Add(new DiagnosisHeadingViewModel(_patientStore, diagnosisHeading));
            }
            return result;
        }

        private ObservableCollection<DiagnosisItemViewModel> DiagnosisItemViewModelProjection(List<Diagnosis> diagnosisList)
        {
            ObservableCollection<DiagnosisItemViewModel>? result = new ObservableCollection<DiagnosisItemViewModel>();
            foreach (var diagnosisItem in diagnosisList)
            {
                result.Add(new DiagnosisItemViewModel(_patientStore, diagnosisItem));
            }
            return result;
        }

    }
}
