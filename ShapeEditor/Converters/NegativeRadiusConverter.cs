using System;
using System.Globalization;
using System.Windows.Data;

namespace ShapeEditor.Converters
{
    public class NegativeRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double radius)
            {
                return -radius;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}