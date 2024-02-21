using PatientManagement.Models.DataEntites;
using System;

namespace PatientManagement.Stores
{
    public class PatientStore
    {
        public event Action<Patient> PatientCreated;
        public event Action<Visit> VisitCreated;

        public void CreatePatient(Patient newPatient)
        {
            PatientCreated?.Invoke(newPatient);
        }

        internal void CreateVisit(Visit newVisit)
        {
            VisitCreated?.Invoke(newVisit);
        }
    }
}
