using PatientManagement.ViewModels.Managers;
using System.Windows;

namespace PatientManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            this.WindowState = WindowState.Maximized;
            this.DataContext = mainViewModel;
        }
    }
}
