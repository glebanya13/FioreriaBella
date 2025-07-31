using FioreriaBella.Services;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class LoginPage : Page
  {
    public LoginPage()
    {
      InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
      UsernameError.Text = "";
      PasswordError.Text = "";

      bool hasError = false;

      if (string.IsNullOrWhiteSpace(UsernameBox.Text))
      {
        UsernameError.Text = "Campo obbligatorio";
        hasError = true;
      }

      if (string.IsNullOrWhiteSpace(PasswordBox.Password))
      {
        PasswordError.Text = "Campo obbligatorio";
        hasError = true;
      }

      if (hasError)
        return;

      var service = new AuthService();
      var user = service.Login(UsernameBox.Text, PasswordBox.Password);

      if (user == null)
      {
        PasswordError.Text = "Credenziali errate.";
        return;
      }

      if (user.Role == "Admin")
        NavigationService?.Navigate(new AdminPage(user.Username, user.Email));
      else
        NavigationService?.Navigate(new UserPage(user.Username, user.Email, user.Role, user.Id));
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new RegisterPage());
    }
  }
}
