using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models.DataEntites
{
    public class History
    {
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        [Key]
        public string Id { get; set; }
        public virtual HistoryHeading HistoryHeading { get; set; } = null!;
        //FK of history heading
        [ForeignKey(nameof(HistoryHeading))]
        public string HistoryHeadingId { get; set; }

        public string HistoryDetail { get; set; }

        public History(string? id, string historyHeadingId, string historyDetail)
        {
            Id = id ?? Guid.NewGuid().ToString();

            HistoryHeadingId = historyHeadingId;
            HistoryDetail = historyDetail;
        }

    }
}
