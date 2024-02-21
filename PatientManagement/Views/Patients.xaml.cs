using PatientManagement.Stores;
using PatientManagement.ViewModels;
using PatientManagement.ViewModels.Managers;
using System;
using System.Windows;

namespace PatientManagement.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Patients : Window
    {
        public PatientListViewModel PatientListViewModel { get; }
        public AddPatientViewModel AddPatientViewModel { get; }
        public Patients()
        {
            InitializeComponent();
            Console.WriteLine("running application");
            this.WindowState = WindowState.Maximized;

            PatientStore patientStore = new PatientStore();

            AddPatientViewModel = new AddPatientViewModel(patientStore);
            PatientListViewModel = new PatientListViewModel(patientStore);
            PatientViewModel patientViewModel = new PatientViewModel(AddPatientViewModel, PatientListViewModel);
            this.DataContext = patientViewModel;

        }


    }
}
