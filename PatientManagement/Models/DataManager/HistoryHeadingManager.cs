using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System.Linq;

namespace PatientManagement.Models.DataManager
{
    public static class HistoryHeadingManager
    {
        public static HistoryHeading AddOrReturnHistoryHeading(string newHeadingText, PatientContext? db)
        {

            db ??= new PatientContext();
            var existingHeading = db.HistoryHeadings.FirstOrDefault(h => h.Heading == newHeadingText);
            // If it doesn't exist, create a new one and set its priority to the lowest
            if (existingHeading == null)
            {
                int newPriority;
                if (db.HistoryHeadings.Any())
                {
                    newPriority = db.HistoryHeadings.Max(h => h.Priority) + 1;
                }
                else
                {
                    // Handle the case where there are no entries in the database
                    // For example, you could set the priority to 1
                    newPriority = 1;
                }
                //higher number lower priority
                existingHeading = new HistoryHeading(null, heading: newHeadingText, priority: newPriority);
                db.HistoryHeadings.Add(existingHeading);
                db.SaveChanges();
            }
            return existingHeading;
        }
    }
}
