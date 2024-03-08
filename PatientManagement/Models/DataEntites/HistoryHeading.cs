using System;
using System.Collections.Generic;

namespace PatientManagement.Models.DataEntites
{
    public class HistoryHeading
    {
        public string Id { get; set; }
        public ICollection<HistoryItem> HistoryItems { get; set; } = null!;

        public HistoryHeading(string? id, string heading, int priority)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Heading = heading;
            Priority = priority;
        }

        public string Heading { get; set; }
        public int Priority { get; set; }
    }

}
