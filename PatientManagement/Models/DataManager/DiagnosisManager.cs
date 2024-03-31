using Microsoft.EntityFrameworkCore;
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
                Diagnosis? diagnosis = db.Diagnoses.Include(d => d.DiagnosisHeading).FirstOrDefault(d => d.Id == diagnosisId);
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

        internal static DiagnosisHeading DisableHeading(DiagnosisHeading diagnosisHeading)
        {
            using (var db = new PatientContext())
            {
                DiagnosisHeading? existingHeading = db.DiagnosisHeadings.FirstOrDefault(d => d.Id == diagnosisHeading.Id);
                if (existingHeading == null) { throw new Exception("Diagnosis Heading does not exist"); }
                existingHeading.IsActive = false;
                db.SaveChanges();
                return existingHeading;

            }
        }

        internal static DiagnosisHeading EnableHeading(DiagnosisHeading diagnosisHeading)
        {
            using (var db = new PatientContext())
            {
                DiagnosisHeading? existingHeading = db.DiagnosisHeadings.FirstOrDefault(d => d.Id == diagnosisHeading.Id);
                if (existingHeading == null) { throw new Exception("Diagnosis Heading does not exist"); }
                existingHeading.IsActive = true;
                db.SaveChanges();
                return existingHeading;

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
                List<Diagnosis> visitDiagnoses =
                    db.Diagnoses.Include(d => d.DiagnosisHeading).Where(d => d.VisitId == currentVisitId).ToList();
                return visitDiagnoses;
            }
        }
        internal static void CreateDiagnosisForNewVisit(string newVisitId)
        {
            using (var db = new PatientContext())
            {
                List<DiagnosisHeading> diagnosisHeadings = db.DiagnosisHeadings.Where(d => d.IsActive == true).ToList();
                foreach (var d in diagnosisHeadings)
                {
                    Diagnosis diagnosis = new Diagnosis(null, string.Empty, newVisitId, d.Id);
                    db.Diagnoses.Add(diagnosis);
                }
                db.SaveChanges();
                //check here if ids were assigned by db

            }
        }

        internal static List<DiagnosisHeading> getAllDiagnosisHeadings()
        {
            using (var db = new PatientContext())
            {
                return db.DiagnosisHeadings.ToList();
            }
        }

        internal static void IncreasePriority(DiagnosisHeading diagnosisHeading)
        {

            using (var db = new PatientContext())
            {
                var transaction = db.Database.BeginTransaction();
                var increaseHeading = db.DiagnosisHeadings.FirstOrDefault(d => d.Id == diagnosisHeading.Id);
                if (increaseHeading == null) { throw new Exception("Diagnosis Heading does not exist"); }

                if (increaseHeading.Priority > 1)
                {
                    int increasedPriority = increaseHeading.Priority - 1;
                    // Find the heading with the priority one less than increaseHeading
                    var decreaseHeading = db.DiagnosisHeadings.FirstOrDefault(d => d.Priority == increasedPriority);

                    //to prevent circular dependency error
                    if (decreaseHeading != null)
                    {
                        decreaseHeading.Priority = -1;
                    }
                    increaseHeading.Priority = -2;
                    db.SaveChanges();

                    if (decreaseHeading != null)
                    {
                        decreaseHeading.Priority = increasedPriority + 1;
                    }
                    increaseHeading.Priority = increasedPriority;
                    db.SaveChanges();
                }

                transaction.Commit();

            }


        }

        internal static List<string>? getAllDiagnosisDetails()
        {
            using (var db = new PatientContext())
            {
                return db.Diagnoses.Select(diagnosis => diagnosis.Detail).Distinct().ToList();
            }
        }



        //internal static void sortPriorities()
        //{
        //    using (var db = new PatientContext())
        //    {
        //        // Query the existing DiagnosisHeadings and order them by priority
        //        List<DiagnosisHeading> diagnosisHeadings = db.DiagnosisHeadings
        //            .OrderBy(d => d.Priority)
        //            .ToList();

        //        // Update priorities sequentially starting from 1
        //        int newPriority = 1;
        //        foreach (var heading in diagnosisHeadings.Where(d => d.IsActive == true))
        //        {
        //            heading.Priority = newPriority;
        //            newPriority++;
        //        }
        //        foreach (var heading in diagnosisHeadings.Where(d => d.IsActive == false))
        //        {
        //            heading.Priority = newPriority;
        //            newPriority++;
        //        }
        //        db.SaveChanges();

        //    }
        //}
    }
}
