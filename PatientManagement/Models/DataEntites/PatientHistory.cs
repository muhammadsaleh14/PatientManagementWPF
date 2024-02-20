namespace PatientManagement.Models.DataEntites
{
    public class PatientHistory
    {
        public string Id { get; set; }
        public PatientHistoryHeading PatientHistoryHeading { get; set; }
        public string PatientHistoryDetail { get; set; }

    }
}
