using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Views.Dialogs;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FioreriaBella.MVVM.ViewModels
{
  public class ManageOrdersViewModel : BaseViewModel
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly NavigationService? _navigation;

    public ObservableCollection<Order> Orders { get; set; }
    public Order? SelectedOrder { get; set; }

    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand BackCommand { get; }

    public ManageOrdersViewModel(IUnitOfWork unitOfWork, NavigationService? navigation)
    {
      _unitOfWork = unitOfWork;
      _navigation = navigation;

      Orders = new ObservableCollection<Order>(_unitOfWork.Orders.GetAll());

      EditCommand = new RelayCommand(EditOrder);
      DeleteCommand = new RelayCommand(DeleteOrder);
      BackCommand = new RelayCommand(_ => Back());
    }

    private void EditOrder(object parameter)
    {
      if (parameter is not Order order) return;

      var dialog = new OrderDialog(order);
      if (dialog.ShowDialog() == true)
      {
        _unitOfWork.Orders.Update(dialog.Order);
        _unitOfWork.SaveChanges();

        int index = Orders.IndexOf(order);
        if (index >= 0)
          Orders[index] = dialog.Order;
      }
    }

    private void DeleteOrder(object parameter)
    {
      if (parameter is not Order order) return;

      var userName = order.User?.Username ?? $"ID {order.UserId}";
      var result = MessageBox.Show(
          $"Вы уверены, что хотите удалить заказ пользователя {userName}?",
          "Подтверждение удаления",
          MessageBoxButton.YesNo,
          MessageBoxImage.Warning);

      if (result == MessageBoxResult.Yes)
      {
        _unitOfWork.Orders.Remove(order);
        _unitOfWork.SaveChanges();
        Orders.Remove(order);
      }
    }

    private void Back()
    {
      _navigation?.GoBack();
    }
  }
}
