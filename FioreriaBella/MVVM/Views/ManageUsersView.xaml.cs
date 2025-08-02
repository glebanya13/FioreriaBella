using FioreriaBella.Data;
using FioreriaBella.Dialogs;
using FioreriaBella.Models;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class ManageUsersPage : Page
  {
    private readonly UserRepository _repo;

    public ManageUsersPage()
    {
      InitializeComponent();
      _repo = new UserRepository();
      LoadUsers();
    }

    private void LoadUsers()
    {
      UsersGrid.ItemsSource = _repo.GetAll();
    }

    private void EditRow_Click(object sender, RoutedEventArgs e)
    {
      if (((FrameworkElement)sender).DataContext is User selectedUser)
      {
        var dialog = new UserDialog(selectedUser);
        if (dialog.ShowDialog() == true)
        {
          _repo.Update(dialog.User);
          LoadUsers();
        }
      }
    }

    private void DeleteRow_Click(object sender, RoutedEventArgs e)
    {
      if (((FrameworkElement)sender).DataContext is User selectedUser)
      {
        var result = MessageBox.Show($"Vuoi eliminare l'utente \"{selectedUser.Username}\"?",
                                     "Conferma eliminazione",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
          _repo.Delete(selectedUser.Id);
          LoadUsers();
        }
      }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
      if (this.NavigationService != null && this.NavigationService.CanGoBack)
      {
        this.NavigationService.GoBack();
      }
      else
      {
        MessageBox.Show("Non è possibile tornare indietro.");
      }
    }
  }
}
