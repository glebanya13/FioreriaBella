using FioreriaBella.Models;
using System.Text.RegularExpressions;
using System.Windows;

namespace FioreriaBella.Dialogs
{
  public partial class UserDialog : Window
  {
    public User User { get; private set; }

    public UserDialog(User user)
    {
      InitializeComponent();

      User = new User
      {
        Id = user.Id,
        Username = user.Username ?? "",
        Email = user.Email ?? "",
        PasswordHash = user.PasswordHash ?? "",
        Role = user.Role ?? ""
      };

      DataContext = User;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      bool isValid = true;

      UsernameError.Text = "";
      EmailError.Text = "";
      RoleError.Text = "";

      if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
      {
        UsernameError.Text = "Inserisci l'username.";
        isValid = false;
      }

      if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || !IsValidEmail(EmailTextBox.Text))
      {
        EmailError.Text = "Inserisci una email valida.";
        isValid = false;
      }

      if (string.IsNullOrWhiteSpace(RoleTextBox.Text))
      {
        RoleError.Text = "Inserisci il ruolo.";
        isValid = false;
      }

      if (!isValid)
        return;

      User.Username = UsernameTextBox.Text.Trim();
      User.Email = EmailTextBox.Text.Trim();
      User.Role = RoleTextBox.Text.Trim();

      DialogResult = true;
    }

    private bool IsValidEmail(string email)
    {
      if (string.IsNullOrWhiteSpace(email))
        return false;

      var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
      return regex.IsMatch(email);
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}
