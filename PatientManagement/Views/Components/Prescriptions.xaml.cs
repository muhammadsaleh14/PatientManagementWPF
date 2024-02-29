using System.Windows.Controls;

namespace PatientManagement.Views.Components
{
    /// <summary>
    /// Interaction logic for Prescriptions.xaml
    /// </summary>
    public partial class Prescriptions : UserControl
    {
        public Prescriptions()
        {
            InitializeComponent();
        }

        //private void AutoCompleteBox_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter && DataContext is PrescriptionsViewModel prescriptionsViewModel)
        //    {
        //        prescriptionsViewModel.AddPrescriptionCommand.Execute(null);
        //    }
        //}
    }
}
