﻿using PatientManagement.Commands;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager;
using PatientManagement.Stores;
using System;
using System.Windows.Input;

namespace PatientManagement.ViewModels.Managers
{
    public class VisitViewModel : ViewModelBase
    {
        public int CanCloseVisitInt = 0;
        public PrescriptionsViewModel PrescriptionsViewModel { get; }
        public HistoryTableViewModel HistoriesViewModel { get; set; }

        public DiagnosisViewModel DiagnosisViewModel { get; set; }

        public ICommand OpenPatientsViewCommand { get; }

        private PatientStore _patientStore;

        private Visit _visit;

        private string _savingText;
        public string SavingText
        {
            get { return _savingText; }
            set
            {
                _savingText = value;
                OnPropertyChanged(nameof(SavingText));
            }
        }


        public Visit Visit
        {
            get { return _visit; }
            set { _visit = value; }
        }

        public Action CloseWindow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public VisitViewModel(PatientStore patientStore)
        {
            _patientStore = patientStore;
            PrescriptionsViewModel = new PrescriptionsViewModel(_patientStore);
            HistoriesViewModel = new HistoryTableViewModel(_patientStore);
            DiagnosisViewModel = new DiagnosisViewModel(_patientStore, false);
            _visit = VisitManager.getVisitDetails(_patientStore.CurrentVisitId);
            OpenPatientsViewCommand = new RelayCommand(OpenPatientsView);
            _patientStore.CanCloseCounter += ChangeCanCloseCounter;
            _savingText = string.Empty;
        }

        private void ChangeCanCloseCounter(bool canClose)
        {
            if (canClose)
            {
                CanCloseVisitInt -= 1;
            }
            else
            {
                CanCloseVisitInt += 1;
            }
            if (CanCloseVisitInt == 0)
            {
                SavingText = string.Empty;
            }
            else
            {
                SavingText = "Saving...";
            }
        }

        private void OpenPatientsView(object obj)
        {
            PatientsViewModel patientsViewModel = new PatientsViewModel(_patientStore);
            _patientStore.ChangeViewModel(patientsViewModel);
        }


    }
}
