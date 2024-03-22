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

        internal static void DeletePatient(Patient patient)
        {
            using (var db = new PatientContext())
            {
                Patient? patientToDelete = db.Patients.Find(patient); // Replace entityId with the ID of the entity you want to delete
                if (patientToDelete != null)
                {
                    db.Patients.Remove(patientToDelete);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Patient not found");
                }
            }
        }

        internal static Patient EditPatient(Patient newPatient)
        {
            using (var db = new PatientContext())
            {
                Patient? patient = db.Patients.FirstOrDefault(p => p.Id == newPatient.Id);
                if (patient != null)
                {
                    patient.Name = newPatient.Name;
                    patient.Age = newPatient.Age;
                    patient.Gender = newPatient.Gender;
                    db.SaveChanges();
                    return patient;
                }
                else
                {
                    throw new Exception("Patient not found");
                }
            }
        }
    }
}
