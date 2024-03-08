using PatientManagement.Models.Contexts;
using PatientManagement.Models.DataEntites;
using System;
using System.Linq;

namespace PatientManagement.Models.DataManager.HelperFuntions
{
    public static class Helper
    {
        public static Visit getVisit(PatientContext db, string visitId)
        {
            Visit? visit = db.Visits
                    .FirstOrDefault(v => v.Id == visitId);

            if (visit == null)
            {
                throw new ArgumentException("Visit does not exist", nameof(visitId));
            }
            return visit;
        }


    }
}
