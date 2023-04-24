using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace CollegeStatictics.Converters
{
    public class JoinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not IEnumerable collection)
                return default!;

            return string.Join(", ", collection);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => default!;
    }
}
