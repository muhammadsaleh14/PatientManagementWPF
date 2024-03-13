using System.Windows;
using System.Windows.Controls;

namespace PatientManagement.Views.Components
{
    /// <summary>
    /// Interaction logic for HistoryControl.xaml
    /// </summary>
    public partial class HistoryControl : UserControl
    {
        public bool IsInputFieldsVisible { get; set; } = false;

        public HistoryControl()
        {

            InitializeComponent();

        }

        private void AddHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleInputVisibility();

        }

        private void ToggleInputVisibility()
        {
            if (spInput.Visibility == Visibility.Visible)
            {
                spInput.Visibility = Visibility.Collapsed;
            }
            else
            {
                spInput.Visibility = Visibility.Visible;
            }


        }
    }
}
