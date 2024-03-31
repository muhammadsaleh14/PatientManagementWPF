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
using System.Windows.Input;

namespace PatientManagement.ViewModels
{
    public class HistoryTableViewModel : ViewModelBase
    {
        private string _visitid;
        private PatientStore _patientStore;

        public List<string> AllHistoryDetails { get; }
        public List<string> AllHistoryHeadings { get; }

        public ICommand AddHistoryCommand { get; }
        public ICommand RemoveHistoryCommand { get; }

        private HistoryTable _historyTable;

        public HistoryTable HistoryTable
        {
            get { return _historyTable; }
            set
            {
                _historyTable = value;
                HistoryItems = ModelToViewModelProjection(_historyTable);

            }
        }

        private ObservableCollection<HistoryItemViewModel> _historyItems;
        public ObservableCollection<HistoryItemViewModel> HistoryItems
        {
            get { return _historyItems; }
            set
            {
                _historyItems = value;
                OnPropertyChanged(nameof(HistoryItems));
            }

        }

        public HistoryTableViewModel(PatientStore patientStore)
        {
            _addHistoryHeadingText = string.Empty;
            _addHistoryDetailText = string.Empty;

            _patientStore = patientStore;
            _visitid = patientStore.CurrentVisitId ?? throw new Exception("Visit Id is null");

            AllHistoryHeadings = HistoryManager.getAllHistoryHeadings() ?? new List<string>();
            AllHistoryDetails = HistoryManager.getAllHistoryDetails() ?? new List<string>();


            _historyTable = HistoryManager.getHistoryTableForVisit(_visitid);
            _patientStore.AllHistoryDetails = AllHistoryDetails;
            _patientStore.AllHistoryHeadings = AllHistoryHeadings;

            _historyItems = ModelToViewModelProjection(_historyTable);

            //_patientStore.HistoryTableChanged += ChangeHistoryTable;
            AddHistoryCommand = new RelayCommand(AddHistory);
            RemoveHistoryCommand = new RelayCommand(RemoveHistory);

        }


        //private void ChangeHistoryTable(HistoryTable obj)
        //{
        //    if (obj is HistoryTable historyTable)
        //    {
        //        HistoryTable = historyTable;
        //    }
        //}

        private ObservableCollection<HistoryItemViewModel> ModelToViewModelProjection(HistoryTable historyTable)
        {
            ObservableCollection<HistoryItemViewModel>? result = new ObservableCollection<HistoryItemViewModel>();
            foreach (var historyItem in historyTable.HistoryItems)
            {
                Debug.WriteLine(historyItem.HistoryHeading.Heading);
                result.Add(new HistoryItemViewModel(_patientStore, historyItem));
            }

            return result;
        }

        private void RemoveHistory(object obj)
        {


            if (obj is HistoryItemViewModel historyItemViewModel && historyItemViewModel.HistoryItem != null)
            {
                HistoryManager.RemoveHistoryItemFromVisit(_visitid, historyItemViewModel.HistoryItem);
                HistoryItems.Remove(historyItemViewModel);
            }

        }

        private void AddHistory(object obj)
        {
            //try
            //{
            HistoryTable historyTable = HistoryManager.AddHistoryItemToVisit(null, _visitid, AddHistoryHeadingText, AddHistoryDetailText);
            HistoryTable = historyTable;
            AddHistoryHeadingText = string.Empty;
            AddHistoryDetailText = string.Empty;

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

        }

        private string _addHistoryHeadingText;

        public string AddHistoryHeadingText
        {
            get { return _addHistoryHeadingText; }
            set
            {
                _addHistoryHeadingText = value;
                OnPropertyChanged(nameof(AddHistoryHeadingText));
            }
        }

        private string _addHistoryDetailText;

        public string AddHistoryDetailText
        {
            get { return _addHistoryDetailText; }
            set
            {
                _addHistoryDetailText = value;
                OnPropertyChanged(nameof(AddHistoryDetailText));
            }
        }

    }
}
