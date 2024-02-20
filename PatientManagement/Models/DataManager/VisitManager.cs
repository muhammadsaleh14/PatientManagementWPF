using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class VisitManager
    {
        public static void AddVisitToPatient(string? patientId)
        {
            using (var db = new PatientContext()) // Use PatientContext instead of VisitContext
            {
                try
                {
                    // Find the patient by ID
                    var patient = db.Patients?.FirstOrDefault(p => p.Id == patientId);
                    if (patient != null)
                    {
                        // Create a new instance of the Visit class
                        Visit newVisit = new Visit()
                        {
                            Date = DateTime.Now
                        };

                        // Add the new visit to the patient's list of visits
                        patient.Visits.Add(newVisit);

                        // Save changes to the database
                        db.SaveChanges();
                    }
                    else
                    {
                        // Handle case where patient with given ID is not found
                        Console.WriteLine($"Patient with ID {patientId} not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    throw;
                }
            }
        }
    }
}
