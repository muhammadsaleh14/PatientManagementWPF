using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models.DataEntites
{
    public class History
    {
        public ICollection<Visit> Visits { get; set; } = null!;


        public HistoryHeading HistoryHeading { get; set; } = null!;
        //FK of history heading
        [ForeignKey(nameof(HistoryHeading))]
        public string HistoryHeadingId { get; set; }


        //Nullable FK
        public HistoryDetail HistoryDetail { get; set; } = null!;
        [ForeignKey(nameof(HistoryDetail))]
        public string? HistoryDetailId { get; set; }

        public History(string historyHeadingId, string historyDetailId)
        {
            HistoryHeadingId = historyHeadingId;
            HistoryDetailId = historyDetailId;
        }

    }
}
