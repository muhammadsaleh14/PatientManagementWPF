using PatientManagement.AutoSaves;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using PatientManagement.ViewModels.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PatientManagement.ViewModels.Items
{
    public class HistoryItemViewModel : ViewModelBase
    {
        private PatientStore _patientStore;

        public List<string> AllHistoryDetails { get; }
        public List<string> AllHistoryHeadings { get; }


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
                    AutoSaveVisit.UpdateText(nameof(HistoryDetail), _historyDetail); // Update text in the timer
                }
            }
        }

        private readonly AutoSaveVisit AutoSaveVisit;

        public HistoryItemViewModel(PatientStore patientStore, HistoryItem historyItem)
        {
            _patientStore = patientStore;
            AllHistoryHeadings = _patientStore.AllHistoryHeadings ?? new List<string>();
            AllHistoryDetails = _patientStore.AllHistoryDetails ?? new List<string>();
            _visitId = patientStore.CurrentVisitId ?? throw new Exception("VisitId is null in history item");
            _historyItem = historyItem;
            _historyHeading = historyItem.HistoryHeading.Heading;
            _historyDetail = historyItem.Detail;
            AutoSaveVisit = new AutoSaveVisit(_patientStore, saveAction);
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
                //_patientStore.ChangeHistoryTable(historyTable);
            }
        }
    }
}
