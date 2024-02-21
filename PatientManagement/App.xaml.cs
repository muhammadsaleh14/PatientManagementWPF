using PatientManagement.Stores;
using PatientManagement.ViewModels;
using PatientManagement.ViewModels.Managers;
using System.Windows;

namespace PatientManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            PatientStore patientStore = new PatientStore();

            AddPatientViewModel addPatientViewModel = new AddPatientViewModel(patientStore);
            PatientListViewModel patientViewModel = new PatientListViewModel(patientStore);
            PatientViewModel mainViewModel = new PatientViewModel(addPatientViewModel, patientViewModel);

            MainWindow = new MainWindow()
            {
                DataContext = mainViewModel
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
