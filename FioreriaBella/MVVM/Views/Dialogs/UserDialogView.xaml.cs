using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views.Dialogs
{
  public partial class UserDialog : Window
  {
    public User Result { get; private set; }

    public UserDialog(User? user = null)
    {
      InitializeComponent();

      var vm = new UserDialogViewModel(user);
      vm.RequestClose += (success, result) =>
      {
        DialogResult = success;
        if (success && result is User u)
          Result = u;
        Close();
      };

      DataContext = vm;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (DataContext is UserDialogViewModel vm)
      {
        vm.Password = ((PasswordBox)sender).Password;
      }
    }
  }
}
