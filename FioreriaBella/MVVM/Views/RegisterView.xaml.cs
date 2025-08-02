using System.Windows.Controls;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.MVVM.Services;
using FioreriaBella.DAL.Interfaces;

namespace FioreriaBella.MVVM.Views
{
  public partial class RegisterView : Page
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      var vm = new RegisterViewModel(_unitOfWork, _userSessionService);
      DataContext = vm;

      vm.RegistrationSuccessful += () =>
      {
        this.NavigationService?.Navigate(new LoginView(_userSessionService, _unitOfWork));
      };

      vm.NavigateToLoginRequested += () =>
      {
        this.NavigationService?.Navigate(new LoginView(_userSessionService, _unitOfWork));
      };
    }

    private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
    {
      if (DataContext is RegisterViewModel vm)
      {
        vm.Password = ((PasswordBox)sender).Password;
      }
    }

    private void ConfirmPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
    {
      if (DataContext is RegisterViewModel vm)
      {
        vm.ConfirmPassword = ((PasswordBox)sender).Password;
      }
    }
  }
}
