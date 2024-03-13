using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class DiagnosisManager
    {
        public static Diagnosis EditDiagnosisDetail(string diagnosisId, string newDiagnosisDetail)
        {
            using (var db = new PatientContext())
            {
                Diagnosis? diagnosis = db.Diagnoses.FirstOrDefault(d => d.Id == diagnosisId);
                if (diagnosis == null) { throw new Exception("No Diagnosis found with the given Id"); }

                diagnosis.Detail = newDiagnosisDetail;
                db.SaveChanges();

                return diagnosis;
            }
        }

        //empry string , if not active
        internal static DiagnosisHeading AddDiagnosisHeading(string addDiagnosisHeadingText)
        {
            using (var db = new PatientContext())
            {

                DiagnosisHeading? diagnosisHeading = db.DiagnosisHeadings.FirstOrDefault(d => d.Heading == addDiagnosisHeadingText);
                if (diagnosisHeading == null)
                {
                    int newPriority;
                    if (db.DiagnosisHeadings.Any())
                    {
                        newPriority = db.DiagnosisHeadings.Max(h => h.Priority) + 1;
                    }
                    else
                    {
                        // Handle the case where there are no entries in the database
                        // For example, you could set the priority to 1
                        newPriority = 1;
                    }
                    //higher number lower priority
                    diagnosisHeading = new DiagnosisHeading(null, addDiagnosisHeadingText, newPriority);
                    db.DiagnosisHeadings.Add(diagnosisHeading);
                    db.SaveChanges();
                    return diagnosisHeading;
                }
                else if (diagnosisHeading.IsActive == false)
                {
                    diagnosisHeading.IsActive = true;
                    db.SaveChanges();
                    return diagnosisHeading;
                }
                else // if already is in actives list
                {
                    throw new Exception("Heading already added");
                }
            }
        }

        internal static void EditDiagnosisHeading(DiagnosisHeading diagnosisHeading)
        {
            using (var db = new PatientContext())
            {
                DiagnosisHeading? existingHeading = db.DiagnosisHeadings.FirstOrDefault(d => d.Id == diagnosisHeading.Id);
                if (existingHeading == null) { throw new Exception("Diagnosis Heading does not exist"); }

                existingHeading.Heading = diagnosisHeading.Heading;
                db.SaveChanges();
            }
        }

        internal static List<Diagnosis> getDiagnosisForVisit(string currentVisitId)
        {
            using (var db = new PatientContext())
            {
                return db.Diagnoses.Where(d => d.VisitId == currentVisitId).ToList();
            }
        }

        internal static List<DiagnosisHeading> getDiagnosisHeadings()
        {
            using (var db = new PatientContext())
            {
                return db.DiagnosisHeadings.Where(d => d.IsActive == true).ToList();
            }
        }
    }
}
