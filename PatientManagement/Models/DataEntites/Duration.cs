using System;

namespace PatientManagement.Models.DataEntites
{
    public class Duration
    {
        public string Id { get; set; }
        public string DurationTime { get; set; }
        public Duration(string? id, string durationTime)
        {
            Id = id ?? Guid.NewGuid().ToString();
            DurationTime = durationTime;
        }
    }
}
