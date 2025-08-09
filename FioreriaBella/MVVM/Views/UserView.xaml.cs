using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class UserView : Page
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public UserView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      var vm = new UserViewModel(_userSessionService, _unitOfWork);
      DataContext = vm;

    vm.OpenMyOrdersRequested += () =>
    {
        this.NavigationService?.Navigate(new MyOrdersView(_userSessionService, _unitOfWork));
    };

    vm.LogoutRequested += () =>
      {
        this.NavigationService?.Navigate(new LoginView(_userSessionService, _unitOfWork));
      };

      vm.OpenCartRequested += () =>
      {
        this.NavigationService?.Navigate(new CartView(_userSessionService, _unitOfWork));
      };

    vm.OpenMyPaymentsRequested += () =>
    {
        this.NavigationService?.Navigate(new MyPaymentsView(_userSessionService, _unitOfWork));
    };
    }
  }
}
