using System;
using System.Globalization;
using System.Windows.Data;

namespace FioreriaBella.MVVM.Converters
{
  public class BoolToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool b)
        return b ? "Да" : "Нет";
      if (value is int i)
        return i != 0 ? "Да" : "Нет";
      return "Нет";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var str = value?.ToString()?.Trim().ToLower();
      return str == "да" || str == "yes" || str == "true";
    }
  }
}
