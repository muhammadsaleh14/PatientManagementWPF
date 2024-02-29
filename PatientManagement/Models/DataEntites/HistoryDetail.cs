using System;

namespace PatientManagement.Models.DataEntites
{
    public class HistoryDetail
    {
        public string Id { get; set; }
        public string Detail { get; set; }

        public HistoryDetail(string? id, string detail)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Detail = detail;
        }
    }
}
