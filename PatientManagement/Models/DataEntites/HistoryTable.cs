using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models.DataEntites
{
    public class HistoryTable
    {
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<HistoryItem> HistoryItems { get; set; } = new List<HistoryItem>();

        [Key]
        public string Id { get; set; }
        public string InitialVisitId { get; set; }

        public HistoryTable(string? id, string initialVisitId)
        {
            Id = id ?? Guid.NewGuid().ToString();
            InitialVisitId = initialVisitId;
        }
    }
}
