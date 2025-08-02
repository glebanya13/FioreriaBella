using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace FioreriaBella.MVVM.Converters
{
  public class BoolToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value is Visibility visibility && visibility == Visibility.Visible;
    }
  }
}
