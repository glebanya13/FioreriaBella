using System;
using System.Windows.Input;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;

namespace FioreriaBella.MVVM.ViewModels
{
  public class AdminViewModel : BaseViewModel
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public string Username => _userSessionService.CurrentUser?.Username ?? "";
    public string Email => _userSessionService.CurrentUser?.Email ?? "";
    public bool IsAdmin => _userSessionService.CurrentUser?.IsAdmin ?? false;

    public ICommand ManageUsersCommand { get; }
    public ICommand ManageProductsCommand { get; }
    public ICommand ManageOrdersCommand { get; }
    public ICommand DashboardCommand { get; }
    public ICommand LogoutCommand { get; }

    public event Action RequestNavigateToUsers;
    public event Action RequestNavigateToProducts;
    public event Action RequestNavigateToOrders;
    public event Action RequestNavigateToDashboard;
    public event Action RequestLogout;

    public AdminViewModel(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      if (_userSessionService.CurrentUser == null)
        throw new InvalidOperationException("CurrentUser is null. Нужно войти.");

      ManageUsersCommand = new RelayCommand(_ => RequestNavigateToUsers?.Invoke());
      ManageProductsCommand = new RelayCommand(_ => RequestNavigateToProducts?.Invoke());
      ManageOrdersCommand = new RelayCommand(_ => RequestNavigateToOrders?.Invoke());
      DashboardCommand = new RelayCommand(_ => RequestNavigateToDashboard?.Invoke());
      LogoutCommand = new RelayCommand(_ => RequestLogout?.Invoke());
    }
  }
}
