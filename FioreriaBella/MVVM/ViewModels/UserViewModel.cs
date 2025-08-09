using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class UserViewModel : BaseViewModel
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public string Username => _userSessionService.CurrentUser?.Username ?? "Пользователь";
    public string Email => _userSessionService.CurrentUser?.Email ?? "no-email";

    public ICommand LogoutCommand { get; }
    public ICommand OpenCartCommand { get; }
    public ICommand OpenMyOrdersCommand { get; }
    public ICommand OpenMyPaymentsCommand { get; }



    public event Action LogoutRequested;
    public event Action OpenCartRequested;
    public event Action OpenMyOrdersRequested;
    public event Action OpenMyPaymentsRequested;

    public UserViewModel(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      _userSessionService = userSessionService ?? throw new ArgumentNullException(nameof(userSessionService));
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

      LogoutCommand = new RelayCommand(_ => Logout());
      OpenMyOrdersCommand = new RelayCommand(_ => OpenMyOrdersRequested?.Invoke());
      OpenMyPaymentsCommand = new RelayCommand(_ => OpenMyPaymentsRequested?.Invoke());
      OpenCartCommand = new RelayCommand(_ => OpenCartRequested?.Invoke());
    }

    private void Logout()
    {
      _userSessionService.ClearUser();
      LogoutRequested?.Invoke();
    }
  }
}
