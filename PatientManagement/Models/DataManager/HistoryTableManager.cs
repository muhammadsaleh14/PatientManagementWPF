using Microsoft.EntityFrameworkCore;
using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public class HistoryTableManager
    {
        internal static HistoryTable AddOrReturnUniqueHistoryTable(PatientContext db, Visit visit)
        {
            // Retrieve existingHistoryTable with related entities
            HistoryTable existingHistoryTable = ReturnHistoryTableForVisit(db, visit);
            // Copy items to a new existingHistoryTable if InitialVisitId doesn't match
            if (existingHistoryTable.InitialVisitId != visit.Id)
            {
                //making new table
                HistoryTable newHistoryTable = new HistoryTable(null, visit.Id);
                db.HistoryTables.Add(newHistoryTable);
                db.SaveChanges();

                // Copying old content to new table
                ICollection<HistoryItem> historyItems = existingHistoryTable.HistoryItems.ToList();
                foreach (var item in historyItems)
                {
                    HistoryItem newItem = new HistoryItem(null, newHistoryTable.Id, item.HistoryHeadingId, item.Detail);
                    db.HistoryItems.Add(newItem);
                }
                db.SaveChanges();


                // Associate the new existingHistoryTable with the newVisit
                visit.HistoryTable = newHistoryTable;
                db.SaveChanges();
            }

            // Ensure finalHistoryTable is retrieved and return it

            HistoryTable? finalHistoryTable = db.HistoryTables
                .Include(h => h.HistoryItems)
                    .ThenInclude(i => i.HistoryHeading)
                .FirstOrDefault(h => h.Id == visit.HistoryTableId);

            if (finalHistoryTable == null)
            {
                throw new InvalidOperationException("History Table not found");
            }
            return finalHistoryTable;
        }

        internal static HistoryTable ReturnHistoryTableForVisit(PatientContext db, Visit visit)
        {
            var dbHistoryTable = db.HistoryTables
                .Include(h => h.HistoryItems)
                        .ThenInclude(i => i.HistoryHeading)
                        .FirstOrDefault(ht => ht.Id == visit.HistoryTableId);
            if (dbHistoryTable != null)
            {
                return dbHistoryTable;
            }


            // Get the patient's visits sorted by date in descending order
            var visits = db.Visits
                .Where(v => v.PatientId == visit.PatientId)
                .OrderByDescending(v => v.Date);

            // Find the latest visit with a non-null HistoryTable
            var latestVisitWithHistoryTable = visits.FirstOrDefault(v => v.HistoryTable != null);

            if (latestVisitWithHistoryTable != null)
            {
                // If a visit with a non-null HistoryTable exists, return its HistoryTable
                var historyTable = db.HistoryTables
                    .Include(h => h.HistoryItems)
                        .ThenInclude(i => i.HistoryHeading)
                    .FirstOrDefault(h => h.Id == latestVisitWithHistoryTable.HistoryTableId);
                // If a visit with a non-null HistoryTable exists, return its HistoryTable
                return historyTable ?? throw new Exception("Visit with history table not found");
            }
            else
            {
                // If no visit with a non-null HistoryTable exists, create a new HistoryTable
                HistoryTable newHistoryTable = new HistoryTable(null, visit.Id);
                db.HistoryTables.Add(newHistoryTable);
                db.SaveChanges();
                db.Entry(newHistoryTable).Collection(h => h.HistoryItems);
                return newHistoryTable;
            }
            // Retrieve existingHistoryTable with related entities
            //HistoryTable? existingHistoryTable = db.HistoryTables
            //    .Include(h => h.HistoryItems)
            //        .ThenInclude(i => i.HistoryHeading)
            //    .FirstOrDefault(h => h.Id == visit.HistoryTableId);

        }

        private static void CopyHistoryItemsToNewTable(HistoryTable sourceTable, Visit newVisit)
        {
            // Create a new existingHistoryTable
            //HistoryTable newHistoryTable = new HistoryTable(null, newVisit.Id);

            //// Copy history items to the new existingHistoryTable
            //newHistoryTable.HistoryItems = sourceTable.HistoryItems.ToList();

            //// Associate the new existingHistoryTable with the newVisit
            //newVisit.HistoryTable = newHistoryTable;
        }

    }
}
