using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FioreriaBella.MVVM.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                bool shouldHide = (parameter as string) == "Отменен" && status == "Отменен";
                bool shouldShowInverted = (parameter as string) == "Invert" && status == "Отменен";

                if (shouldHide) return Visibility.Collapsed;
                if (shouldShowInverted) return Visibility.Visible;

                return (parameter as string) == "Отменен" ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}