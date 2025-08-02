using System.Windows;

namespace FioreriaBella
{
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      base.OnStartup(e);
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      MessageBox.Show(e.ExceptionObject.ToString(), "Ошибка приложения", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }
}
