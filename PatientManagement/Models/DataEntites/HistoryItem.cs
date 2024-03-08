using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models.DataEntites
{
    public class HistoryItem
    {
        [ForeignKey(nameof(HistoryHeading))]
        public string HistoryHeadingId { get; set; }
        public HistoryHeading HistoryHeading { get; set; } = null!;


        [ForeignKey(nameof(HistoryTable))]
        public string HistoryTableId { get; set; }
        public HistoryTable HistoryTable { get; set; } = null!;


        [Key]
        public string Id { get; set; }
        public string Detail { get; set; }

        public HistoryItem(string? id, string historyTableId, string historyHeadingId, string detail)
        {
            Id = id ?? Guid.NewGuid().ToString();
            HistoryTableId = historyTableId;
            HistoryHeadingId = historyHeadingId;
            Detail = detail;
        }
    }
}
