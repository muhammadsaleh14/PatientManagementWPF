using PatientManagement.Models.DataEntites;
using PatientManagement.ViewModels;
using System.Diagnostics;
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
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Debug.WriteLine("Selection changed" + sender);
            var a = (ListView)sender;
            //Debug.WriteLine("Selected value" + a.SelectedValue);
            if (sender is ListView listView && listView.SelectedItem is Patient selectedPatient)
            {
                Debug.WriteLine("selected item name:" + selectedPatient.Name);
                if (DataContext is PatientListViewModel patientListViewModel)
                {
                    patientListViewModel.SelectedPatient = selectedPatient; // Update the selected patient's ID property in the view model
                }

            }

        }
    }
}
