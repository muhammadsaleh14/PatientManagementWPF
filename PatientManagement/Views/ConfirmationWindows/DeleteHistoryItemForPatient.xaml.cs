using System.Windows;

namespace PatientManagement.Views.ConfirmationWindows
{
    /// <summary>
    /// Interaction logic for DeleteHistoryItemForPatient.xaml
    /// </summary>
    public partial class DeleteHistoryItemForPatient : Window
    {
        public bool isVisit { get; private set; } = true;
        public bool isCancelled { get; private set; } = true;

        public DeleteHistoryItemForPatient()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Delete_For_Patient_Click(object sender, RoutedEventArgs e)
        {
            isVisit = false;
            isCancelled = false;
            Close();
        }
        private void Delete_For_Visit_Click(object sender, RoutedEventArgs e)
        {
            isVisit = true;
            isCancelled = false;
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            isCancelled = true;
            Close();
        }
    }
}
