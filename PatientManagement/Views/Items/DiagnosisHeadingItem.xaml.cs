using System.Windows;
using System.Windows.Controls;

namespace PatientManagement.Views.Items
{
    /// <summary>
    /// Interaction logic for DiagnosisHeadingItem.xaml
    /// </summary>
    public partial class DiagnosisHeadingItem : UserControl
    {
        private bool _editMode = false;
        public DiagnosisHeadingItem()
        {
            InitializeComponent();
            ToggleEditMode();
        }

        private void editHeadingBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditMode();
        }

        private void submitNewHeadingBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            if (_editMode == true)
            {
                textBoxHeading.Visibility = Visibility.Visible;
                textBlockHeading.Visibility = Visibility.Collapsed;
                //editHeadingBtn.Visibility= Visibility.Collapsed;
                submitNewHeadingBtn.Visibility = Visibility.Visible;
                _editMode = false;
            }
            else
            {
                textBoxHeading.Visibility = Visibility.Collapsed;
                textBlockHeading.Visibility = Visibility.Visible;
                //editHeadingBtn.Visibility= Visibility.Visble;
                submitNewHeadingBtn.Visibility = Visibility.Collapsed;
                _editMode = true;
            }

        }
    }
}
