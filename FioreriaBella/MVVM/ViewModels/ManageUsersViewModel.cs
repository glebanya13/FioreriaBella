using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class ManageUsersViewModel : BaseViewModel
  {
    private readonly IUnitOfWork _unitOfWork;

    public ObservableCollection<User> Users { get; }

    public ICommand AddUserCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand BackCommand { get; }

    public event Action RequestAddUser;
    public event Action<User> RequestEditUser;
    public event Action<User> RequestDeleteUser;
    public event Action RequestBack;

    public ManageUsersViewModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;

      Users = new ObservableCollection<User>(_unitOfWork.Users.GetAll());

      AddUserCommand = new RelayCommand(_ => RequestAddUser?.Invoke());
      EditCommand = new RelayCommand(u => RequestEditUser?.Invoke(u as User));
      DeleteCommand = new RelayCommand(u => RequestDeleteUser?.Invoke(u as User));
      BackCommand = new RelayCommand(_ => RequestBack?.Invoke());
    }

    public void RefreshUsers()
    {
      Users.Clear();
      foreach (var user in _unitOfWork.Users.GetAll())
        Users.Add(user);
    }
  }
}
