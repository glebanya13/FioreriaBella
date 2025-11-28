using System.IO;

namespace FioreriaBella.MVVM.Services
{
  public static class ImageService
  {
    private static readonly string ImagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Products");

    static ImageService()
    {
      // Создаем папку для изображений, если её нет
      if (!Directory.Exists(ImagesDirectory))
      {
        Directory.CreateDirectory(ImagesDirectory);
      }
    }

    /// <summary>
    /// Копирует изображение в локальную папку и возвращает относительный путь
    /// </summary>
    public static string? SaveImage(string? sourcePath)
    {
      if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
      {
        return null;
      }

      try
      {
        // Получаем расширение файла
        var extension = Path.GetExtension(sourcePath);
        if (string.IsNullOrEmpty(extension))
        {
          extension = ".jpg"; // По умолчанию
        }

        // Генерируем уникальное имя файла
        var fileName = $"{Guid.NewGuid()}{extension}";
        var destinationPath = Path.Combine(ImagesDirectory, fileName);

        // Копируем файл
        File.Copy(sourcePath, destinationPath, overwrite: true);

        // Возвращаем относительный путь для сохранения в БД
        return Path.Combine("Images", "Products", fileName);
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Получает полный путь к изображению по относительному пути из БД
    /// </summary>
    public static string? GetImagePath(string? relativePath)
    {
      if (string.IsNullOrWhiteSpace(relativePath))
      {
        return null;
      }

      // Если это уже полный путь или URL, возвращаем как есть
      if (Path.IsPathRooted(relativePath) || relativePath.StartsWith("http://") || relativePath.StartsWith("https://"))
      {
        return relativePath;
      }

      // Если это относительный путь, создаем полный путь
      var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
      
      // Проверяем существование файла
      if (File.Exists(fullPath))
      {
        return fullPath;
      }

      return null;
    }

    /// <summary>
    /// Удаляет изображение по относительному пути
    /// </summary>
    public static void DeleteImage(string? relativePath)
    {
      if (string.IsNullOrWhiteSpace(relativePath))
      {
        return;
      }

      try
      {
        var fullPath = GetImagePath(relativePath);
        if (fullPath != null && File.Exists(fullPath) && !Path.IsPathRooted(relativePath))
        {
          File.Delete(fullPath);
        }
      }
      catch
      {
        // Игнорируем ошибки при удалении
      }
    }
  }
}

