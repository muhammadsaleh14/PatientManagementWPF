using PatientManagement.Models.DataEntites;
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
    }
}
