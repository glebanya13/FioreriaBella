using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System.Collections.ObjectModel;
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

        public event Action? BackRequested;

        public MyOrdersViewModel(IUnitOfWork unitOfWork, UserSessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _sessionService = sessionService;

            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());

            LoadOrders();
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
