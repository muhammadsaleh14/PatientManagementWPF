using System.Windows;
using System.Windows.Input;

namespace PatientManagement.Views
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {

        public AddPatient()
        {
            InitializeComponent();
        }

        private void age_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!char.IsDigit(e.Text, e.Text.Length - 1) && !char.IsControl(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }


    }
}
