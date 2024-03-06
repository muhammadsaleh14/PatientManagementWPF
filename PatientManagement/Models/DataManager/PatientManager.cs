using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class PatientManager
    {
        public static Patient addPatientToDb(Patient patient)
        {
            using (var db = new PatientContext())
            {

                patient.DateCreated = DateTime.Now;
                db.Add(patient);
                db.SaveChanges();
                Debug.WriteLine("patient id is:", patient.Id);
                return patient;


            }
        }
        public static List<Patient> getPatientsFromDb()
        {
            using (var db = new PatientContext())
            {
                var patients = db.Patients != null ?
                   db.Patients
                              .Select(p => new Patient(p.Id, p.Name, p.Age, p.Gender))
                              .ToList() :
                   new List<Patient>();// Return an empty list if db.Patients is null
                return patients;
            }
        }


    }
}
