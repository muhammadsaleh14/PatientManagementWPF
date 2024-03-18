using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace PatientManagement.ViewModels.Items
{
    public class HistoryItemViewModel : ViewModelBase
    {
        private PatientStore _patientStore;

        private string _visitId;

        private HistoryItem _historyItem;

        public HistoryItem HistoryItem
        {
            get { return _historyItem; }
            set
            {
                // Perform some action, such as logging or validation
                _historyItem = value;

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

        public HistoryItemViewModel(PatientStore patientStore, HistoryItem historyItem)
        {
            _patientStore = patientStore;
            _visitId = patientStore.CurrentVisitId ?? throw new Exception("VisitId is null in history item");
            _historyItem = historyItem;
            _historyHeading = historyItem.HistoryHeading.Heading;
            _historyDetail = historyItem.Detail;
            AutoSaveTimer = new AutoSaveTimer(saveAction);
            AutoSaveTimer.PropertyChanged += AutoSaveTimerChanged;

        }
        private void AutoSaveTimerChanged(object? sender, PropertyChangedEventArgs e)
        {

            if (sender is AutoSaveTimer autoSaveTimer && e.PropertyName == nameof(autoSaveTimer.IsSaving))
            {
                _patientStore.ChangeCanCloseCounter(!autoSaveTimer.IsSaving);
            }
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
                HistoryTable historyTable = HistoryManager.EditHistoryDetailForVisit(_visitId, _historyItem.Id, textValue);
                _patientStore.ChangeHistoryTable(historyTable);
            }
        }
    }
}
