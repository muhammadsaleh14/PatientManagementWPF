using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using System;
using System.Diagnostics;

namespace PatientManagement.ViewModels.Items
{
    public class HistoryItemViewModel : ViewModelBase
    {
        private string _visitId;


        private History _history;

        public History History
        {
            get { return _history; }
            set
            {
                // Perform some action, such as logging or validation
                _history = value;

            }
        }
        private string _historyHeading;
        public string HistoryHeading
        {
            get { return _historyHeading; }
            set
            {
                if (_historyHeading != value)
                {
                    _historyHeading = value;
                    OnPropertyChanged(nameof(HistoryHeading));
                }
            }
        }

        // String property for HistoryHeading

        // String property for HistoryDetail
        private string _historyDetail;
        public string HistoryDetail
        {
            get { return _historyDetail; }
            set
            {
                if (_historyDetail != value)
                {
                    _historyDetail = value;
                    OnPropertyChanged(nameof(HistoryDetail));
                    AutoSaveTimer.UpdateText(nameof(HistoryDetail), _historyDetail); // Update text in the timer
                }
            }
        }

        private readonly AutoSaveTimer AutoSaveTimer;

        public HistoryItemViewModel(PatientStore patientStore, History history)
        {
            _visitId = patientStore.CurrentVisitId ?? throw new Exception("VisitId is null in history item");
            _history = history;
            _historyHeading = history.HistoryHeading.Heading;
            _historyDetail = history.HistoryDetail;
            AutoSaveTimer = new AutoSaveTimer(saveAction);
        }

        private void saveAction(string propertyName, string? textValue)
        {
            Debug.WriteLine("Running save on:" + propertyName + " value is:" + textValue);
            if (textValue == null)
            {
                Debug.WriteLine("Parameter textValue is null for History save action");
                return;
            }
            if (propertyName == nameof(HistoryDetail))
            {
                History = HistoryManager.EditHistoryDetailForVisit(_visitId, _history.Id, textValue);
            }
        }
    }
}
