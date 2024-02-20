using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PatientManagement.Converters
{
    public class RadioButtonConverter : IValueConverter
    {
        public RadioButtonConverter() { }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter); // Check if the selected value matches the radio button's content
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter; // Return the selected value as is
        }
    }

}
