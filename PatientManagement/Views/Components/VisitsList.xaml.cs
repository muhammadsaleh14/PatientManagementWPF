using PatientManagement.Models.DataEntites;
using PatientManagement.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace PatientManagement.Views.Components
{
    /// <summary>
    /// Interaction logic for VisitsList.xaml
    /// </summary>
    public partial class VisitsList : UserControl
    {
        public VisitsList()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem
                && DataContext is VisitListViewModel visitListViewModel
                && listViewItem.DataContext is Visit item)
            {
                //Debug.WriteLine("Visit id that was chosen" + item.Id + " PatientName:" + item.Patient.Name);
                visitListViewModel.OpenVisitViewCommand.Execute(item.Id);
                // Access the item that was double-clicked (item)
                // Perform any necessary actions...
            }

        }
    }
}
