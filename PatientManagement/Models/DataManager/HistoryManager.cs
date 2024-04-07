using Microsoft.EntityFrameworkCore;
using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using PatientManagement.Models.DataManager.HelperFuntions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class HistoryManager
    {
        internal static HistoryTable AddHistoryItemToVisit(PatientContext? context, string visitId, string addHistoryHeadingText, string addHistoryDetailText)
        {
            if (string.IsNullOrWhiteSpace(addHistoryHeadingText))
            {
                throw new ArgumentException("Heading can't be empty", nameof(addHistoryHeadingText));
            }

            using (var db = context ?? new PatientContext())
            {
                // Get the currentVisit

                Visit visit = Helper.getVisit(db, visitId);

                //Get visits HistoryTable
                HistoryTable historyTable = HistoryTableManager.AddOrReturnUniqueHistoryTable(db, visit);

                visit.HistoryTable = historyTable;

                bool isHeadingAlreadyAdded = visit.HistoryTable.HistoryItems.Any(i => i.HistoryHeading.Heading == addHistoryHeadingText);
                if (isHeadingAlreadyAdded)
                {
                    throw new Exception("Cannot add duplicate heading");
                }

                //Does heading exist in the headings table
                HistoryHeading historyHeading = HistoryHeadingManager.AddOrReturnHistoryHeading(addHistoryHeadingText, db);

                if (visit.HistoryTableId == null)
                {
                    throw new Exception("Visits HistoryTable Id is null");
                }

                HistoryItem historyItem = new HistoryItem(null, visit.HistoryTableId, historyHeading.Id, addHistoryDetailText);
                db.HistoryItems.Add(historyItem);
                db.SaveChanges();

                //currentVisit.HistoryTable.HistoryItems.Add(newHistoryItem);
                //db.SaveChanges();

                return visit.HistoryTable;
            }
        }



        //If the heading has changed make a new one or assign the existing ones's id
        //also remove the heading that was changed from the currentVisit
        internal static HistoryTable EditHistoryHeadingForVisit(string visitId, string oldHeading, string newHeading)
        {
            if (string.IsNullOrWhiteSpace(newHeading))
            {
                throw new ArgumentException("Heading can't be empty", nameof(newHeading));
            }

            using (var db = new PatientContext())
            {

                Visit visit = Helper.getVisit(db, visitId);

                HistoryTable historyTable = HistoryTableManager.AddOrReturnUniqueHistoryTable(db, visit);

                // Remove the history
                visit.HistoryTable = historyTable;

                HistoryItem? historyItem = visit.HistoryTable.HistoryItems.FirstOrDefault(i => i.HistoryHeading.Heading == oldHeading);

                if (historyItem == null)
                {
                    throw new Exception("No such history record exists:" + oldHeading);
                }

                bool isHeadingAlreadyAdded = visit.HistoryTable.HistoryItems.Any(i => i.HistoryHeading.Heading == newHeading);
                if (isHeadingAlreadyAdded)
                {
                    throw new Exception("Cannot add duplicate heading");
                }
                HistoryHeading newHistoryHeading = HistoryHeadingManager.AddOrReturnHistoryHeading(newHeading, db);
                historyItem.HistoryHeading = newHistoryHeading;

                db.SaveChanges();

                // Add a new history with the updated heading


                return visit.HistoryTable;

            }
        }



        internal static HistoryTable EditHistoryDetailForVisit(string visitId, HistoryItem historyItem, string newDetail)
        {
            using (var db = new PatientContext())
            {

                if (newDetail == null)
                {
                    throw new ArgumentException("New Detail is null", nameof(newDetail));
                }

                Visit visit = Helper.getVisit(db, visitId);

                HistoryTable historyTable = HistoryTableManager.AddOrReturnUniqueHistoryTable(db, visit);

                visit.HistoryTable = historyTable;

                string? matchByHeadingString = db.HistoryItems.FirstOrDefault(h => h.Id == historyItem.Id)?.HistoryHeading.Heading;
                if (matchByHeadingString == null)
                {
                    throw new Exception("Heading not found");
                }
                HistoryItem? newHistoryItem = visit.HistoryTable.HistoryItems.FirstOrDefault(i => i.HistoryHeading.Heading == matchByHeadingString);

                if (newHistoryItem == null)
                {
                    throw new Exception("No such history record exists with id:" + newHistoryItem);
                }

                newHistoryItem.Detail = newDetail;

                db.SaveChanges();

                // Add a new history with the updated detail

                return visit.HistoryTable;
            }
        }




        internal static HistoryTable getHistoryTableForVisit(string visitId)
        {
            using (var db = new PatientContext())
            {
                Visit visit = Helper.getVisit(db, visitId);

                HistoryTable historyTable = HistoryTableManager.ReturnHistoryTableForVisit(db, visit);


                visit.HistoryTable = historyTable;

                db.SaveChanges();

                return visit.HistoryTable;
            }
        }

        internal static void RemoveHistoryItemFromVisit(string visitId, HistoryItem historyItemParam)
        {
            using (var db = new PatientContext())
            {

                Visit visit = Helper.getVisit(db, visitId);

                HistoryTable historyTable = HistoryTableManager.AddOrReturnUniqueHistoryTable(db, visit);

                visit.HistoryTable = historyTable;

                HistoryItem? historyItem = visit.HistoryTable.HistoryItems.FirstOrDefault(i => i.Id == historyItemParam.Id);

                if (historyItem == null)
                {
                    throw new Exception("No such history record exists with Id:" + historyItem);
                }

                visit.HistoryTable.HistoryItems.Remove(historyItem);
                db.SaveChanges();

            }
        }

        internal static List<string>? getAllHistoryDetails()
        {
            using (var db = new PatientContext())
            {
                return db.HistoryItems.Select(history => history.Detail).Distinct().ToList();
            }
        }

        internal static List<string>? getAllHistoryHeadings()
        {
            using (var db = new PatientContext())
            {
                return db.HistoryHeadings.Select(historyHeading => historyHeading.Heading).Distinct().ToList();
            }
        }

        internal static void RemoveHistoryItemFromPatient(string visitId, HistoryItem historyItem)
        {
            using (var db = new PatientContext())
            {
                Visit? currentVisit = db.Visits.FirstOrDefault(v => v.Id == visitId);
                HistoryItem? dbHistoryItem = db.HistoryItems.Include(h => h.HistoryHeading).FirstOrDefault(h => h.Id == historyItem.Id);

                if (currentVisit == null)
                {
                    throw new Exception("Visit not found");
                }
                if (dbHistoryItem == null)
                {
                    throw new Exception("History Item with heading not found");
                }

                var patientVisits = db.Visits.Where(v => v.PatientId == currentVisit.PatientId)
                    .Include(v => v.HistoryTable)
                    .ThenInclude(ht => ht!.HistoryItems)
                    .ThenInclude(hi => hi.HistoryHeading);

                foreach (Visit localVisit in patientVisits)
                {
                    if (localVisit.HistoryTable == null)
                    {
                        continue;
                    }
                    var itemToRemove = localVisit.HistoryTable.HistoryItems.FirstOrDefault(h => h.HistoryHeading.Heading == dbHistoryItem.HistoryHeading.Heading);

                    if (itemToRemove != null)
                    {
                        // Remove the history item from the list
                        localVisit.HistoryTable.HistoryItems.Remove(itemToRemove);

                        // Save changes to the database
                    }
                }
                db.SaveChanges();

            }
        }

    }
}
