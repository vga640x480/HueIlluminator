using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace HueIlluminator.UI
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BooleanToInvisibilityConverter : IValueConverter
    {
    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
        var boolValue = (bool)value;
            return boolValue ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        return null;
    }
    }
}
