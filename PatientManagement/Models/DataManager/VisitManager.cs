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
        public static List<Visit>? GetPatientVisitsFromDb(string? patientId)
        {
            using (var db = new PatientContext())
            {
                return db.Visits?
                    .Where(v => v.PatientId == patientId)
                    .Select(v => new Visit(v.Id, v.PatientId, v.OptionalDetail, v.Date))
                    .ToList();
            }
        }
        public static Visit? AddVisitToPatient(string? patientId)
        {
            using (var db = new PatientContext()) // Use PatientContext instead of VisitContext
            {

                // Find the visits by ID
                if (patientId != null)
                {
                    var visits = db.Visits;
                    Debug.WriteLine("patient ID" + patientId);

                    // Create a new instance of the VisitPage class
                    Visit newVisit = new(id: null, patientId: patientId, optionalDetail: null, date: DateTime.Now);

                    // Add the new visit to the visits's list of visits
                    visits.Add(newVisit);
                    // Save changes to the database
                    db.SaveChanges();

                    // Reload the new visit from the database to get the generated Id
                    db.Entry(newVisit).GetDatabaseValues();

                    DiagnosisManager.CreateDiagnosisForNewVisit(newVisit.Id);

                    return newVisit;
                }
                else
                {
                    // Handle case where visits with given ID is not found
                    Console.WriteLine($"Patient with ID null {patientId} not found.");
                    return null;
                }


            }
        }

        internal static Visit getVisitDetails(string? currentVisitId)
        {
            using (var db = new PatientContext())
            {
                // Query the database to get the visit with the specified ID
                var visit = db.Visits.FirstOrDefault(v => v.Id == currentVisitId);

                if (visit != null)
                {
                    // Query the database to get the patient associated with the visit
                    var patient = db.Patients.FirstOrDefault(p => p.Id == visit.PatientId);

                    if (patient != null)
                    {
                        // Create a new instance of the Visit class with patient details
                        Visit visitDetails = new Visit(visit.Id, visit.PatientId, visit.OptionalDetail, visit.Date)
                        {
                            Patient = patient
                        };

                        return visitDetails;
                    }
                    else
                    {
                        // Handle case where patient with the associated ID is not found
                        throw new Exception("Patient with this id does not exist");
                    }
                }
                else
                {
                    throw new Exception("Visit with this id does not exist");
                }
            }

        }

        internal static string? SaveDetail(Visit visit, string? value)
        {
            using (var db = new PatientContext())
            {
                Visit? dbVisit = db.Visits.Find(visit.Id);
                if (dbVisit != null)
                {
                    dbVisit.OptionalDetail = value;
                    db.SaveChanges();
                    return dbVisit.OptionalDetail;
                }
                else
                {
                    throw new Exception("Visit not found");
                }
            }
        }

        internal static void DeleteVisit(Visit visit)
        {
            using (var db = new PatientContext())
            {

                Visit? dbVisit = db.Visits.Find(visit.Id);
                if (dbVisit != null)
                {
                    db.Visits.Remove(dbVisit);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Visit not found");
                }
            }
        }
    }
}
