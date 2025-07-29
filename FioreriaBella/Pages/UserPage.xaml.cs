using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class UserPage : Page
  {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public UserPage(string username, string email, string role)
    {
      InitializeComponent();
      Username = username;
      Email = email;
      Role = role;
      DataContext = this;
    }

    private void Logout_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      NavigationService?.Navigate(new LoginPage());
    }
  }
}
