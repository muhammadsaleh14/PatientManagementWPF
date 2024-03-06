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
    public class HistoriesViewModel : ViewModelBase
    {
        private string _visitid;
        private PatientStore _patientStore;
        public ICommand AddHistoryCommand { get; }
        public ICommand RemoveHistoryCommand { get; }

        private ObservableCollection<HistoryItemViewModel> _histories;
        public ObservableCollection<HistoryItemViewModel> Histories
        {
            get { return _histories; }
            set
            {
                _histories = value;
                OnPropertyChanged(nameof(Histories));
            }

        }

        public HistoriesViewModel(PatientStore patientStore)
        {
            _addHistoryHeadingText = string.Empty;
            _addHistoryDetailText = string.Empty;
            _patientStore = patientStore;
            _visitid = patientStore.CurrentVisitId ?? throw new Exception("Visit Id is null");
            _histories = ModelToViewModelProjection(HistoryManager.getPatientHistoryForVisit(_visitid));
            AddHistoryCommand = new RelayCommand(AddHistory);
            RemoveHistoryCommand = new RelayCommand(RemoveHistory);

        }

        private ObservableCollection<HistoryItemViewModel> ModelToViewModelProjection(IEnumerable<History> historyList)
        {
            ObservableCollection<HistoryItemViewModel>? result = new ObservableCollection<HistoryItemViewModel>();
            foreach (var history in historyList)
            {
                Debug.WriteLine(history.HistoryHeading.Heading);
                result.Add(new HistoryItemViewModel(_patientStore, history));
            }

            return result;
        }

        private void RemoveHistory(object obj)
        {

            try
            {
                if (obj is HistoryItemViewModel historyItemViewModel)
                {
                    HistoryManager.RemoveHistoryFromVisit(_visitid, historyItemViewModel.History.Id);
                    Histories.Remove(historyItemViewModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddHistory(object obj)
        {
            try
            {
                History history = HistoryManager.AddHistoryToVisit(null, _visitid, AddHistoryHeadingText, AddHistoryDetailText);
                HistoryItemViewModel historyItemViewModel = new HistoryItemViewModel(_patientStore, history);
                Histories.Add(historyItemViewModel);

                AddHistoryHeadingText = string.Empty;
                AddHistoryDetailText = string.Empty;

            }
            catch (Exception ex)
            {

                throw ex;
            }

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
