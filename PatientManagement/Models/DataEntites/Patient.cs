using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models.DataEntites
{
    public class Patient
    {
        [Key]

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public DateTime? DateCreated { get; set; }
        public ObservableCollection<Visit> Visits { get; set; } = new ObservableCollection<Visit>();
    }
}
