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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class DiagnosisViewModel : ViewModelBase
    {
        private PatientStore _patientStore;


        public ICommand AddDiagnosisHeadingCommand { get; }

        private string _messageText = string.Empty;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                if (_messageText != value)
                {

                    _messageText = value;
                    OnPropertyChanged(nameof(MessageText));
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

        private bool _showDisabledHeadings;

        public bool ShowDisabledHeadings
        {
            get { return _showDisabledHeadings; }
            set
            {
                if (_showDisabledHeadings != value)
                {
                    _showDisabledHeadings = value;
                    OnPropertyChanged(nameof(ShowDisabledHeadings));
                    // Update visibility of items when checkbox state 
                }
            }
        }
        private ObservableCollection<DiagnosisHeadingViewModel> _allDiagnosisHeadings;
        public ObservableCollection<DiagnosisHeadingViewModel> AllDiagnosisHeadings
        {
            get { return _allDiagnosisHeadings; }
            set
            {
                _allDiagnosisHeadings = value;
                OnPropertyChanged(nameof(AllDiagnosisHeadings));
            }
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
            _allDiagnosisHeadings = new ObservableCollection<DiagnosisHeadingViewModel>();

            AddDiagnosisHeadingCommand = new RelayCommand(AddDiagnosisHeading, CanAddDiagnosisHeading);
            if (!isConfigWindow)
            {
                _patientStore.AllDiagnosisDetails = DiagnosisManager.getAllDiagnosisDetails();
                string currentVisitId = _patientStore.CurrentVisitId ?? throw new Exception("Visitid is not availible for diagnosis");
                _diagnosisItems = DiagnosisItemViewModelProjection(DiagnosisManager.getDiagnosisForVisit(currentVisitId));
            }
            else
            {
                _patientStore.ErrorDiagosisHeadingViewModel += (string error) => MessageText = error;
                _patientStore.DiagnosisHeadingPriorityChanged += RefreshDiagnosisHeadingList;
                _allDiagnosisHeadings = DiagnosisHeadingViewModelProjection(DiagnosisManager.getAllDiagnosisHeadings());

            }
        }



        private void RefreshDiagnosisHeadingList()
        {
            AllDiagnosisHeadings = DiagnosisHeadingViewModelProjection(DiagnosisManager.getAllDiagnosisHeadings());
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

                var itemToRemove = AllDiagnosisHeadings.FirstOrDefault(diagnosisViewModel => diagnosisViewModel.DiagnosisHeading.Id == diagnosisHeading.Id);
                if (itemToRemove != null)
                {
                    AllDiagnosisHeadings.Remove(itemToRemove);
                }

                AllDiagnosisHeadings.Add(new DiagnosisHeadingViewModel(_patientStore, diagnosisHeading));
                AddDiagnosisHeadingText = string.Empty;

            }
            catch (Exception ex)
            {
                MessageText = ex.Message;
                await Task.Delay(1000);
                MessageText = string.Empty;
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
