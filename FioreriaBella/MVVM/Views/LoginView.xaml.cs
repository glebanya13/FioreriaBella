using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class LoginView : Page
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      var vm = new LoginViewModel(_unitOfWork, _userSessionService);
      DataContext = vm;

      vm.LoginSuccess += () =>
      {
        if (_userSessionService.CurrentUser?.IsAdmin == true)
        {
          NavigationService?.Navigate(new AdminView(_userSessionService, _unitOfWork));
        }
        else
        {
          NavigationService?.Navigate(new UserView(_userSessionService, _unitOfWork));
        }
      };

      vm.RegisterRequested += () =>
      {
        this.NavigationService?.Navigate(new RegisterView(_userSessionService, _unitOfWork));
      };
    }

    private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
    {
      if (DataContext is LoginViewModel vm)
      {
        vm.Password = ((PasswordBox)sender).Password;
      }
    }
  }
}
