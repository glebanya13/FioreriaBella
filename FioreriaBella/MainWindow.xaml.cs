using FioreriaBella.Pages;
using System.Windows;
using System.Windows.Input;

namespace FioreriaBella
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      MainFrame.Navigate(new LoginPage());
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}
