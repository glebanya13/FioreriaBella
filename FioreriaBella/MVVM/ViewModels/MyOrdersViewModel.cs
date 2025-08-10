using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
    public class MyOrdersViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserSessionService _sessionService;

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        private Order? _selectedOrder;
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        public ICommand BackCommand { get; }
        public ICommand CancelOrderCommand { get; }

        public event Action? BackRequested;
        public event Action? OrderCancelled;

        public MyOrdersViewModel(IUnitOfWork unitOfWork, UserSessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _sessionService = sessionService;

            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
            CancelOrderCommand = new RelayCommand(CancelOrder);

            LoadOrders();
        }

        private void CancelOrder(object parameter)
        {
            if (parameter is not Order order || order.Status == "Отменен")
                return;

            var result = MessageBox.Show(
                $"Вы уверены, что хотите отменить заказ #{order.Id}?",
                "Подтверждение отмены",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                order.Status = "Отменен";
                _unitOfWork.Orders.Update(order);
                _unitOfWork.SaveChanges();

                LoadOrders(); // Обновляем список заказов
                OrderCancelled?.Invoke();

                MessageBox.Show("Заказ успешно отменен", "Отмена заказа",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadOrders()
        {
            Orders.Clear();

            var userId = _sessionService.CurrentUser?.Id;
            if (userId == null) return;

            var orders = _unitOfWork.Orders
                .Find(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            foreach (var order in orders)
            {
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
    }
}