using PatientManagement.Models.DataEntites;
using System;

namespace PatientManagement.Stores
{
    class PatientStore
    {
        public event Action<Patient> PatientCreated;

        public void CreatePatient(Patient patient)
        {
            PatientCreated?.Invoke(patient);
        }
    }
}
