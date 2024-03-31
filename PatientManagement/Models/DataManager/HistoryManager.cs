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
                // Get the visit

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

                //visit.HistoryTable.HistoryItems.Add(historyItem);
                //db.SaveChanges();

                return visit.HistoryTable;
            }
        }



        //If the heading has changed make a new one or assign the existing ones's id
        //also remove the heading that was changed from the visit
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



        internal static HistoryTable EditHistoryDetailForVisit(string visitId, string historyItemId, string newDetail)
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

                HistoryItem? historyItem = visit.HistoryTable.HistoryItems.FirstOrDefault(i => i.Id == historyItemId);

                if (historyItem == null)
                {
                    throw new Exception("No such history record exists with id:" + historyItemId);
                }

                historyItem.Detail = newDetail;

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
    }
}
