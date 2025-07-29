using FioreriaBella.Services;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class RegisterPage : Page
  {
    public RegisterPage()
    {
      InitializeComponent();
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
      bool isValid = true;

      UsernameError.Text = "";
      EmailError.Text = "";
      PasswordError.Text = "";

      if (string.IsNullOrWhiteSpace(UsernameBox.Text))
      {
        UsernameError.Text = "Inserisci un nome utente.";
        isValid = false;
      }

      string email = EmailBox.Text;
      if (string.IsNullOrWhiteSpace(email))
      {
        EmailError.Text = "Inserisci un'email.";
        isValid = false;
      }
      else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
      {
        EmailError.Text = "Formato email non valido.";
        isValid = false;
      }

      if (string.IsNullOrWhiteSpace(PasswordBox.Password))
      {
        PasswordError.Text = "Inserisci una password.";
        isValid = false;
      }

      if (!isValid) return;

      try
      {
        var authService = new AuthService();
        bool registered = authService.Register(UsernameBox.Text.Trim(), email.Trim(), PasswordBox.Password);

        if (registered)
        {
          MessageBox.Show("Registrazione avvenuta con successo!", "Registrazione", MessageBoxButton.OK, MessageBoxImage.Information);
          NavigationService?.Navigate(new LoginPage());
        }
        else
        {
          MessageBox.Show("Registrazione fallita: nome utente già esistente.", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Errore durante la registrazione: {ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new LoginPage());
    }
  }
}
