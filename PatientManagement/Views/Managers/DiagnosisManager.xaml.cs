using System.Windows;
using System.Windows.Controls;

namespace PatientManagement.Views.Managers
{
    /// <summary>
    /// Interaction logic for DiagnosisManager.xaml
    /// </summary>
    public partial class DiagnosisManager : UserControl
    {
        public DiagnosisManager()
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
