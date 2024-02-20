using PatientManagement.Models.DataEntites;
using PatientManagement.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PatientManagement.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Patients : Window
    {
        public Patients()
        {
            InitializeComponent();
            Console.WriteLine("running application");
            this.WindowState = WindowState.Maximized;
            PatientViewModel patientViewModel = PatientViewModel.Instance;
            this.DataContext = patientViewModel;

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
