using System.Windows.Controls;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.MVVM.Services;
using FioreriaBella.DAL.Interfaces;

namespace FioreriaBella.MVVM.Views
{
  public partial class AdminView : Page
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public AdminView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      var vm = new AdminViewModel(_userSessionService, _unitOfWork);
      DataContext = vm;

      vm.RequestNavigateToUsers += () =>
      {
        this.NavigationService?.Navigate(new ManageUsersView(_userSessionService, _unitOfWork));
      };
      vm.RequestNavigateToProducts += () =>
      {
        this.NavigationService?.Navigate(new ManageProductsView(_userSessionService, _unitOfWork));
      };
      vm.RequestNavigateToOrders += () =>
      {
        this.NavigationService?.Navigate(new ManageOrdersView(_userSessionService, _unitOfWork));
      };
      vm.RequestLogout += () =>
      {
        _userSessionService.ClearUser();
        this.NavigationService?.Navigate(new LoginView(_userSessionService, _unitOfWork));
      };
    }
  }

}
