using System.Windows;

namespace PatientManagement.Views.ConfirmationWindows
{
    /// <summary>
    /// Interaction logic for DeleteConfirmationWindow.xaml
    /// </summary>
    /// 
    public partial class DeleteConfirmationWindow : Window
    {
        public bool Confirmed { get; private set; }

        public DeleteConfirmationWindow()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = false;
            Close();
        }
    }
}
