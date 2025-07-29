using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FioreriaBella.Pages
{
  public partial class AdminPage : Page
  {
    private readonly string _username;
    private readonly string _email;

    public AdminPage(string username, string email)
    {
      InitializeComponent();
      _username = username;
      _email = email;

      UsernameText.Text = $"Utente: {_username}";
      EmailText.Text = $"Email: {_email}";
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new LoginPage());
    }

    private void ManageUsers_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new ManageUsersPage());
    }

    private void ManageOrders_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new ManageOrdersPage());
    }

    private void ManageProducts_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new ManageProductsPage());
    }

    private void Dashboard_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Panoramica amministrativa", "Dashboard", MessageBoxButton.OK, MessageBoxImage.Information);
    }
  }
}
