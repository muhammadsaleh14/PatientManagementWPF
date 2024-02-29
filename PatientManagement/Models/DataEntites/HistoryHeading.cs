using System;
using System.Collections.Generic;

namespace PatientManagement.Models.DataEntites
{
    public class HistoryHeading
    {
        public string Id { get; set; }
        public ICollection<History> Histories { get; set; } = null!;

        public HistoryHeading(string? id, string heading)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Heading = heading;
        }

        public string Heading { get; set; }
        public int Priority { get; set; }
    }

}
