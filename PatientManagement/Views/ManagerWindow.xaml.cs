using System.Windows;

namespace PatientManagement.Views
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            AddNewDiagnosisForm.Visibility = Visibility.Collapsed;
        }



        private void AddNewDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            if (AddNewDiagnosisForm.Visibility == Visibility.Visible)
            {
                AddNewDiagnosisForm.Visibility = Visibility.Collapsed;
            }
            else
            {
                AddNewDiagnosisForm.Visibility = Visibility.Visible;
            }
        }
    }
}
