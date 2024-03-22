using PatientManagement.Models.DataEntites;
using PatientManagement.ViewModels.Items;
using PatientManagement.ViewModels.Managers;
using System;

namespace PatientManagement.Stores
{
    public class PatientStore
    {
        public string? CurrentVisitId;
        public event Action<ViewModelBase>? ViewModelChanged;
        public event Action<Patient>? PatientCreated;
        public event Action<Visit>? VisitCreated;
        public event Action<PatientStore>? AddPatientWindowOpened;
        public event Action<Patient?>? PatientSelectionChanged;
        public event Action<HistoryTable>? HistoryTableChanged;
        public event Action<DiagnosisHeadingViewModel>? DiagnosisHeadingDisabled;
        public event Action? DiagnosisHeadingPriorityChanged;
        public event Action<Patient>? PatientEdited;

        public event Action<bool>? CanCloseCounter;


        internal void EditPatient(Patient patient)
        {
            PatientEdited?.Invoke(patient);
        }
        internal void ChangeDiagnosisHeadingPriority()
        {
            DiagnosisHeadingPriorityChanged?.Invoke();
        }

        internal void ChangeCanCloseCounter(bool canClose)
        {
            CanCloseCounter?.Invoke(canClose);
        }

        internal void DisableHeading(DiagnosisHeadingViewModel diagnosisHeadingViewModel)
        {
            DiagnosisHeadingDisabled?.Invoke(diagnosisHeadingViewModel);
        }
        public void ChangeHistoryTable(HistoryTable historyTable)
        {
            HistoryTableChanged?.Invoke(historyTable);
        }
        public void ChangeViewModel(ViewModelBase currentViewModel)
        {
            ViewModelChanged?.Invoke(currentViewModel);
        }
        public void CreatePatient(Patient newPatient)
        {
            PatientCreated?.Invoke(newPatient);
        }

        internal void CreateVisit(Visit newVisit)
        {
            VisitCreated?.Invoke(newVisit);
        }

        internal void AddPatientWindowOpen(PatientStore patientStore)
        {
            AddPatientWindowOpened?.Invoke(patientStore);
        }

        internal void ChangePatientSelection(Patient? patient)
        {
            PatientSelectionChanged?.Invoke(patient);
        }

        internal void EditPatient(object oldPatient, Patient newPatient)
        {
            throw new NotImplementedException();
        }
    }
}
