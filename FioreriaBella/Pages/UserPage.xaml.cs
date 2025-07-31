using FioreriaBella.Models;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class UserPage : Page
  {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public int UserId { get; set; }

    public UserPage(string username, string email, string role, int userId)
    {
      InitializeComponent();
      Username = username;
      Email = email;
      Role = role;
      UserId = userId;
      DataContext = this;
    }

    private void ExploreCatalog_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new CatalogPage(UserId));
    }

    private void Cart_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new CartPage());
    }

    private void Wishlist_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new WishlistPage());
    }

    private void Logout_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      NavigationService?.Navigate(new LoginPage());
    }
  }
}
