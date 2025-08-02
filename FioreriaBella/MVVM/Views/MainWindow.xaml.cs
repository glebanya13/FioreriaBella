using FioreriaBella.DAL;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.DAL.Repositories;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.MVVM.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FioreriaBella.MVVM.Views
{
  public partial class MainWindow : Window
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;
    private MainViewModel ViewModel;

    public MainWindow()
    {
      InitializeComponent();

      _userSessionService = new UserSessionService();
      _unitOfWork = new UnitOfWork(ApplicationContext.Instance);

      ViewModel = new MainViewModel(MainFrame, _userSessionService, _unitOfWork);
      DataContext = ViewModel;

      MainFrame.Navigate(new LoginView(_userSessionService, _unitOfWork));

      MainFrame.Navigated += MainFrame_Navigated;

      ViewModel.IsNavigationVisible = false;
    }

    private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
      if (e.Content is Page currentPage)
      {
        ViewModel.IsNavigationVisible = !(currentPage is LoginView || currentPage is RegisterView);
      }
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }
  }
}
