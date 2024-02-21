using PatientManagement.ViewModels;
using System;
using System.Windows;

namespace PatientManagement.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Patients : Window
    {
        public PatientViewModel PatientViewModel { get; set; }
        public Patients()
        {
            InitializeComponent();
            Console.WriteLine("running application");
            this.WindowState = WindowState.Maximized;

            PatientViewModel = PatientViewModel.Instance;
            this.DataContext = PatientViewModel;

        }


    }
}
