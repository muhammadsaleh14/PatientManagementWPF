﻿using PatientManagement.Models.Contexts;
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
                try
                {
                    patient.DateCreated = DateTime.Now;
                    db.Add(patient);
                    db.SaveChanges();
                    Debug.WriteLine("patient id is:", patient.Id);
                    return patient;
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot insert duplicate");
                }

            }
        }
        public static List<Patient> getPatientsFromDb()
        {
            using (var db = new PatientContext())
            {
                return db.Patients.ToList();
            }
        }

    }
}
