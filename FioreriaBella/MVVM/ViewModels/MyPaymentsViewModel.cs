using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
    public class MyPaymentsViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserSessionService _sessionService;

        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();

        private Payment? _selectedPayment;
        public Payment? SelectedPayment
        {
            get => _selectedPayment;
            set => SetProperty(ref _selectedPayment, value);
        }

        public ICommand BackCommand { get; }
        public ICommand LeaveReviewCommand { get; }

        public event Action? BackRequested;
        public event Action<Product>? LeaveReviewRequested;

        public MyPaymentsViewModel(IUnitOfWork unitOfWork, UserSessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _sessionService = sessionService;

            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
            LeaveReviewCommand = new RelayCommand(product =>
            {
                if (product is Product p)
                {
                    LeaveReviewRequested?.Invoke(p);
                }
            });

            LoadPayments();
        }

        private void LoadPayments()
        {
            Payments.Clear();

            var userId = _sessionService.CurrentUser?.Id;
            if (userId == null) return;

            var orders = _unitOfWork.Orders
                .Find(o => o.UserId == userId)
                .ToList();

            foreach (var order in orders)
            {
                var payments = _unitOfWork.Payments
                    .Find(p => p.OrderId == order.Id)
                    .OrderByDescending(p => p.PaymentDate)
                    .ToList();

                foreach (var payment in payments)
                {
                    payment.Order = order;
                    payment.Order.OrderItems = _unitOfWork.OrderItems
                        .Find(oi => oi.OrderId == order.Id)
                        .ToList();

                    foreach (var item in payment.Order.OrderItems)
                    {
                        item.Product = _unitOfWork.Products.GetById(item.ProductId);
                    }

                    Payments.Add(payment);
                }
            }
        }
    }
}