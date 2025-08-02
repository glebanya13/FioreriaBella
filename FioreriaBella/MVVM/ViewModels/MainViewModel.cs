using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.Views;
using System.Windows.Navigation;

namespace FioreriaBella.MVVM.ViewModels
{
  public class MainViewModel : BaseViewModel
  {
    public Frame MainFrame { get; }

    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    private bool _isNavigationVisible = true;
    public bool IsNavigationVisible
    {
      get => _isNavigationVisible;
      set => SetProperty(ref _isNavigationVisible, value);
    }

    public ICommand NavigateProfileCommand { get; }
    public ICommand NavigateCatalogCommand { get; }
    public ICommand ExitCommand { get; }

    public MainViewModel(Frame frame, UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      MainFrame = frame;
      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      NavigateProfileCommand = new RelayCommand(_ => NavigateProfile());
      NavigateCatalogCommand = new RelayCommand(_ => NavigateCatalog());
      ExitCommand = new RelayCommand(_ => ExitApp());
    }

    private void NavigateProfile()
    {
      if (_userSessionService.CurrentUser?.IsAdmin == true)
      {
        MainFrame.Navigate(new AdminView(_userSessionService, _unitOfWork));
      }
      else
      {
        MainFrame.Navigate(new UserView(_userSessionService, _unitOfWork));
      }
    }

    private void NavigateCatalog()
    {
      MainFrame.Navigate(new Views.CatalogView(_userSessionService, _unitOfWork));
    }

    private void ExitApp()
    {
      Application.Current.Shutdown();
    }
  }
}
