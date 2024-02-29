using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    static public class PrescriptionManager
    {
        internal static List<string> getAllMedicineNames()
        {
            using (var db = new PatientContext())
            {
                return db.Medicines
                    .Select(m => m.MedicineName)
                    .ToList() ?? new List<string>();

            }
        }

        internal static List<string> getAllDosageValues()
        {
            using (var db = new PatientContext())
            {
                return db.Dosages
                    .Select(d => d.Dose)
                    .ToList() ?? new List<string>();

            }
        }

        internal static List<string> getAllDurationTimes()
        {
            using (var db = new PatientContext())
            {
                return db.Durations
                    .Select(d => d.DurationTime)
                    .ToList() ?? new List<string>();

            }
        }

        public static Prescription AddPrescriptionToVisit(string visitId, string AddMedicineText, string AddDosageText, string AddDurationText)
        {
            using (var db = new PatientContext())
            {

                Prescription result;
                // Check if medicine, dosage, and duration already exist
                var medicine = db.Medicines.FirstOrDefault(m => m.MedicineName == AddMedicineText);
                var dosage = db.Dosages.FirstOrDefault(d => d.Dose == AddDosageText);
                var duration = db.Durations.FirstOrDefault(d => d.DurationTime == AddDurationText);

                if (medicine == null)
                {
                    medicine = new Medicine(null, AddMedicineText);
                    db.Medicines.Add(medicine);
                }

                if (dosage == null)
                {
                    dosage = new Dosage(null, AddDosageText);
                    db.Dosages.Add(dosage);
                }

                if (duration == null)
                {
                    duration = new Duration(null, AddDurationText);
                    db.Durations.Add(duration);
                }

                db.SaveChanges();

                //if exists then assign the prescription to the visit
                //then if already assigned throw error
                //Else make new prescription
                //then Assign that new prescription to the visit

                //Check if prescription exists
                var existingPrescription = db.Prescriptions.FirstOrDefault(p =>
                    p.MedicineId == medicine.Id
                    && p.DosageId == dosage.Id
                    && p.DurationId == duration.Id);
                var visit = db.Visits
                    .FirstOrDefault(v => v.Id == visitId);

                if (visit == null)
                {
                    throw new Exception("Incorrect VisitId-Prescription cannot be assigned-Terminating");
                }
                if (existingPrescription != null)
                {
                    var assigned = existingPrescription.Visits.Any(v => v.Id == visitId);

                    if (assigned == true)
                    {
                        throw new Exception("Prescription has already been added to this visit");
                    }
                    else
                    {
                        existingPrescription.Visits.Add(visit);
                        db.SaveChanges();
                        result = existingPrescription;

                    }

                }
                //If existingPrescription does not exist :-{}
                else
                {
                    var newPrescription = new Prescription(medicine.Id, dosage.Id, duration.Id);
                    newPrescription.Visits.Add(visit);
                    db.Prescriptions.Add(newPrescription);
                    db.SaveChanges();
                    result = newPrescription;
                }

                return result;

            }
        }

        internal static List<Prescription> getPatientVisitPrescriptions(string visitId)
        {
            using (var db = new PatientContext())
            {
                var prescriptions = (from p in db.Prescriptions
                                     where p.Visits.Any(v => v.Id == visitId)
                                     join m in db.Medicines on p.MedicineId equals m.Id
                                     join d in db.Dosages on p.DosageId equals d.Id
                                     join dur in db.Durations on p.DurationId equals dur.Id
                                     select new Prescription(p.MedicineId, p.DosageId, p.DurationId)
                                     {
                                         Medicine = m,
                                         Dosage = d,
                                         Duration = dur
                                     }).ToList() ?? new List<Prescription>();
                return prescriptions;
            }
        }

        internal static void DeletePrescription(Prescription prescription)
        {
            using (var db = new PatientContext())
            {
                try
                {
                    db.Prescriptions.Remove(prescription);
                    db.SaveChanges();
                }
                catch
                {
                    throw new Exception("Error deleting prescription from db");
                }
            }
        }
    }
}
