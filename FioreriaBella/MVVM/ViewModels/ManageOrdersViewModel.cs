using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Views.Dialogs;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
    public class ManageOrdersViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObservableCollection<Order> Orders { get; set; }
        public Order? SelectedOrder { get; set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }

        public event Action? BackRequested;

        public ManageOrdersViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Orders = new ObservableCollection<Order>();

            EditCommand = new RelayCommand(EditOrder);
            DeleteCommand = new RelayCommand(DeleteOrder);
            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());

            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders.Clear();

            var orders = _unitOfWork.Orders.GetAll();
            var users = _unitOfWork.Users.GetAll().ToDictionary(u => u.Id);

            foreach (var order in orders.OrderByDescending(o => o.OrderDate))
            {
                if (users.TryGetValue(order.UserId, out var user))
                {
                    order.User = user;
                }

                order.OrderItems = _unitOfWork.OrderItems
                    .Find(oi => oi.OrderId == order.Id)
                    .ToList();

                foreach (var item in order.OrderItems)
                {
                    item.Product = _unitOfWork.Products.GetById(item.ProductId);
                }

                Orders.Add(order);
            }
        }

        private void EditOrder(object parameter)
        {
            if (parameter is not Order order) return;

            var orderCopy = new Order
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                User = order.User
            };

            var dialog = new OrderDialog(orderCopy);
            if (dialog.ShowDialog() == true && dialog.Order != null)
            {
                try
                {
                    var existingOrder = _unitOfWork.Orders.GetById(order.Id);
                    if (existingOrder != null)
                    {
                        existingOrder.Status = dialog.Order.Status;
                        _unitOfWork.Orders.Update(existingOrder);
                        _unitOfWork.SaveChanges();
                        LoadOrders();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении заказа: {ex.Message}",
                                  "Ошибка",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error);
                }
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
    }
}