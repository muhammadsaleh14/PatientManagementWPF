using PatientManagement.Models.DataEntites;
using PatientManagement.ViewModels;
using System.Windows.Controls;

namespace PatientManagement.Views.Components
{
    /// <summary>
    /// Interaction logic for PatientsList.xaml
    /// </summary>
    public partial class PatientsList : UserControl
    {
        public PatientsList()
        {
            InitializeComponent();
            this.DataContext = PatientViewModel.Instance;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Patient selectedPatient)
            {
                if (DataContext is PatientViewModel patientViewModel)
                {
                    patientViewModel.SelectedPatientId = selectedPatient.Id; // Update the selected patient's ID property in the view model
                }
            }
        }
    }
}
