using PatientManagement.ViewModels;
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
            this.DataContext = PatientViewModel.Instance;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
