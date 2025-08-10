using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
    public class ProductReviewsViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _productId;

        public ObservableCollection<ProductReview> Reviews { get; } = new ObservableCollection<ProductReview>();
        public string ProductName { get; set; }

        public ICommand BackCommand { get; }
        public ICommand DeleteReviewCommand { get; }

        public event Action? BackRequested;

        public ProductReviewsViewModel(IUnitOfWork unitOfWork, int productId)
        {
            _unitOfWork = unitOfWork;
            _productId = productId;

            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
            DeleteReviewCommand = new RelayCommand(DeleteReview);

            LoadReviews();
        }

        private void LoadReviews()
        {
            Reviews.Clear();
            var product = _unitOfWork.Products.GetById(_productId);
            ProductName = product?.Name ?? "Товар";

            var reviews = _unitOfWork.ProductReviews
                .Find(r => r.ProductId == _productId)
                .ToList();

            var userIds = reviews.Select(r => r.UserId).Distinct().ToList();
            var users = _unitOfWork.Users
                .Find(u => userIds.Contains(u.Id))
                .ToDictionary(u => u.Id);

            foreach (var review in reviews)
            {
                if (users.TryGetValue(review.UserId, out var user))
                {
                    review.User = user;
                }
                Reviews.Add(review);
            }
        }

        private void DeleteReview(object parameter)
        {
            if (parameter is not ProductReview review) return;

            var result = MessageBox.Show(
                $"Удалить отзыв пользователя {review.User?.Username}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _unitOfWork.ProductReviews.Remove(review);
                _unitOfWork.SaveChanges();
                Reviews.Remove(review);
            }
        }
    }
}