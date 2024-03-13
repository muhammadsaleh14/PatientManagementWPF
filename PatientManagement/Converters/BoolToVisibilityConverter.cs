using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PatientManagement.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
            {
                return isVisible ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                // Handle unexpected value type (optional)
                return Visibility.Collapsed;  // Or throw an exception for clarity
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Implement conversion back logic if needed (optional)
            throw new NotImplementedException();  // Not required for most scenarios
        }
    }

}
