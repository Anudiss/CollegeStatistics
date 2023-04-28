using CollegeStatictics.ViewModels;
using ModernWpf.Controls;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CollegeStatictics.Converters
{
    public class PathToIconConverter : IValueConverter
    {
        private static GeometryConverter _geomertyConverter = new();

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string pageName == false)
                return null;

            var pageIconPath = MainVM.PageIcons[pageName];

            return new PathIcon
            {
                Data = _geomertyConverter.ConvertFromString(pageIconPath) as Geometry
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
