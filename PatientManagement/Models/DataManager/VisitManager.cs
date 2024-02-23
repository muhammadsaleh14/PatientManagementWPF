using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class VisitManager
    {
        public static List<Visit>? getVisitsFromDb(string? patientId)
        {
            using (var db = new PatientContext())
            {
                return db.Visits?.ToList();
            }
        }
        public static Visit? AddVisitToPatient(string? patientId)
        {
            using (var db = new PatientContext()) // Use PatientContext instead of VisitContext
            {
                try
                {
                    // Find the patient by ID

                    var patient = db.Patients?.FirstOrDefault(p => p.Id == patientId);
                    Debug.WriteLine("patient ID" + patientId + " Patient:" + patient?.Name);
                    if (patient != null)
                    {
                        // Create a new instance of the Visit class
                        Visit newVisit = new Visit(date: DateTime.Now);

                        // Add the new visit to the patient's list of visits
                        patient.Visits.Add(newVisit);

                        // Save changes to the database
                        db.SaveChanges();

                        // Reload the new visit from the database to get the generated Id
                        db.Entry(newVisit).GetDatabaseValues();

                        return newVisit;
                    }
                    else
                    {
                        // Handle case where patient with given ID is not found
                        Console.WriteLine($"Patient with ID {patientId} not found.");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    throw;
                }
            }
        }

        internal static List<Visit> getPatientsVisitFromDb(string? patientId)
        {
            using (var db = new PatientContext())
            {
                // Assuming Visit has a PatientId property
                return db.Visits
                         .Where(v => v.PatientId == patientId)
                         .Select(v => new Visit(v.Date)
                         {
                             Id = v.Id,
                             PatientId = v.PatientId,
                             OptionalDetail = v.OptionalDetail
                         })
                         .ToList();
            }
        }
    }
}
