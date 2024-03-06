using Microsoft.EntityFrameworkCore;
using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class HistoryManager
    {
        internal static History AddHistoryToVisit(PatientContext? context, string visitId, string addHistoryHeadingText, string addHistoryDetailText)
        {
            if (string.IsNullOrWhiteSpace(addHistoryHeadingText))
            {
                throw new ArgumentException("Heading can't be empty", nameof(addHistoryHeadingText));
            }

            using (var db = context ?? new PatientContext())
            {
                // Check if history already exists
                var existingHistory = db.Histories
                    .Include(h => h.HistoryHeading)
                    .FirstOrDefault(h =>
                        h.HistoryHeading.Heading == addHistoryHeadingText &&
                        h.HistoryDetail == addHistoryDetailText
                    );

                if (existingHistory == null)
                {
                    // If history doesn't exist, add it
                    HistoryHeading historyHeading = HistoryHeadingManager.AddOrReturnHistoryHeading(addHistoryHeadingText, db);
                    existingHistory = new History(null, historyHeading.Id, addHistoryDetailText)
                    {
                        HistoryHeading = historyHeading
                    };
                    db.Histories.Add(existingHistory);
                }

                // Get the visit
                Visit? visit = db.Visits.FirstOrDefault(v => v.Id == visitId);
                if (visit == null)
                {
                    throw new ArgumentException("Visit does not exist", nameof(visitId));
                }

                // Add the visit to the history
                existingHistory.Visits.Add(visit);
                db.SaveChanges();

                return existingHistory;
            }
        }

        //If the heading has changed make a new one or assign the existing ones's id
        //also remove the heading that was changed from the visit
        internal static History EditHistoryHeadingForVisit(string visitId, string historyId, string newHeading)
        {
            if (string.IsNullOrWhiteSpace(newHeading))
            {
                throw new ArgumentException("Heading can't be empty", nameof(newHeading));
            }

            using (var db = new PatientContext())
            {


                var visit = db.Visits.FirstOrDefault(v => v.Id == visitId);
                if (visit == null)
                {
                    throw new ArgumentException("Visit does not exist", nameof(visitId));
                }

                var historyToRemove = visit.Histories.FirstOrDefault(h => h.Id == historyId);
                if (historyToRemove == null)
                {
                    throw new ArgumentException("History to replace is null", nameof(historyId));
                }

                // Remove the history
                visit.Histories.Remove(historyToRemove);
                db.SaveChanges();

                // Add a new history with the updated heading
                var newHistory = AddHistoryToVisit(db, visitId, newHeading, historyToRemove.HistoryDetail);


                return newHistory;

            }
        }



        internal static History EditHistoryDetailForVisit(string visitId, string historyId, string newDetail)
        {
            using (var db = new PatientContext())
            {

                if (newDetail == null)
                {
                    throw new ArgumentException("New Detail is null", nameof(newDetail));
                }
                var visit = db.Visits
                    .Include(v => v.Histories)
                        .ThenInclude(h => h.HistoryHeading)
                    .FirstOrDefault(v => v.Id == visitId);

                if (visit == null)
                {
                    throw new ArgumentException("Visit does not exist", nameof(visitId));
                }

                var historyToRemove = visit.Histories.FirstOrDefault(h => h.Id == historyId);
                if (historyToRemove == null)
                {
                    throw new ArgumentException("History to replace is null", nameof(historyId));
                }

                // Store the heading for later use
                string heading = historyToRemove.HistoryHeading.Heading;

                // Remove the history
                visit.Histories.Remove(historyToRemove);
                db.SaveChanges();

                // Add a new history with the updated detail
                var newHistory = AddHistoryToVisit(db, visitId, heading, newDetail);

                return newHistory;
            }
        }




        internal static IEnumerable<History> getPatientHistoryForVisit(string visitId)
        {
            using (var db = new PatientContext())
            {
                return db.Histories
                    .Include(h => h.HistoryHeading)
                    .Where(h => h.Visits.Any(v => v.Id == visitId))
                    //.OrderBy(h => h.HistoryHeading.Priority) // Sort by Priority in HistoryHeading
                    .ToList();
            }
        }

        internal static void RemoveHistoryFromVisit(string visitId, string historyId)
        {
            using (var db = new PatientContext())
            {
                var historyToRemove = db.Visits
                .Where(v => v.Id == visitId)
                .SelectMany(v => v.Histories)
                .FirstOrDefault(h => h.Id == historyId);

                if (historyToRemove == null)
                {
                    throw new Exception("History to remove is null");
                }
                db.Entry(historyToRemove).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }
    }
}
