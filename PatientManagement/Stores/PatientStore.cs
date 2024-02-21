using PatientManagement.Models.DataEntites;
using System;

namespace PatientManagement.Stores
{
    public class PatientStore
    {
        public event Action<Patient> PatientCreated;
        public event Action<Visit> VisitCreated;
        public event Action<PatientStore> AddPatientWindowOpened;

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
    }
}
