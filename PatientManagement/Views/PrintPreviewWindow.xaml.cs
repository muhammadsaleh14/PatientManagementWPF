using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PatientManagement.Views
{
    /// <summary>
    /// Interaction logic for PrintPreviewWindow.xaml
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        private const double DefaultZoom = 0.7;
        public PrintPreviewWindow(FrameworkElement content)
        {
            InitializeComponent();
            ContentControl.Content = content;
            SetZoom(DefaultZoom);
        }

        public void SetZoom(double zoom)
        {
            ScaleTransform scale = new ScaleTransform(zoom, zoom);
            ContentControl.LayoutTransform = scale;
        }
        public FrameworkElement getContent()
        {
            return (FrameworkElement)ContentControl.Content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual((FrameworkElement)ContentControl.Content, "Printing...");
            }
        }
    }
}
