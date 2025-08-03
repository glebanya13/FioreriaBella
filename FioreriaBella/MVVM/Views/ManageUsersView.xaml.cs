using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.MVVM.Views.Dialogs;
using FioreriaBella.Models.Entities;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class ManageUsersView : Page
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionService _userSessionService;
    private readonly ManageUsersViewModel _viewModel;

    public ManageUsersView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _unitOfWork = unitOfWork;
      _userSessionService = userSessionService;

      _viewModel = new ManageUsersViewModel(_unitOfWork);
      DataContext = _viewModel;

      _viewModel.RequestAddUser += () =>
      {
        var dialog = new UserDialog();
        if (dialog.ShowDialog() == true && dialog.Result is User newUser)
        {
          _unitOfWork.Users.Add(newUser);
          _unitOfWork.SaveChanges();
          _viewModel.Users.Add(newUser);
        }
      };

      _viewModel.RequestEditUser += user =>
      {
        if (user != null)
        {
          var dialog = new UserDialog(user);
          if (dialog.ShowDialog() == true && dialog.Result is User updatedUser)
          {
            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.IsAdmin = updatedUser.IsAdmin;

            _unitOfWork.Users.Update(user);
            _unitOfWork.SaveChanges();
          }
        }
      };

      _viewModel.RequestDeleteUser += user =>
      {
        if (user != null &&
            MessageBox.Show($"Вы действительно хотите удалить пользователя «{user.Username}»?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
          _unitOfWork.Users.Remove(user);
          _unitOfWork.SaveChanges();
          _viewModel.Users.Remove(user);
        }
      };

      _viewModel.RequestBack += () =>
      {
        this.NavigationService?.GoBack();
      };
    }
  }
}
