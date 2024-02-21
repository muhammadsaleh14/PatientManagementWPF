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

        public Patients()
        {
            InitializeComponent();
            Console.WriteLine("running application");
            this.WindowState = WindowState.Maximized;

            PatientViewModel patientViewModel = new PatientViewModel();
            this.DataContext = patientViewModel;

        }


    }
}
