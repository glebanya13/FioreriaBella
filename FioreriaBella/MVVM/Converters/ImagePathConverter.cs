using FioreriaBella.MVVM.Services;
using System.Globalization;
using System.Windows.Data;

namespace FioreriaBella.MVVM.Converters
{
  public class ImagePathConverter : IValueConverter
  {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      if (value is string path && !string.IsNullOrWhiteSpace(path))
      {
        // Получаем полный путь к изображению через ImageService
        var fullPath = ImageService.GetImagePath(path);
        return fullPath;
      }
      return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}

