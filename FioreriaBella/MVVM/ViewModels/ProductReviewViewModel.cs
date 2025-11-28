using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using FioreriaBella.Models.Entities;
using System;
using System.Windows;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
    public class ProductReviewViewModel : BaseViewModel
    {
        private readonly UserSessionService _sessionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Product _product;
        private bool _hasExistingReview;
        private ProductReview? _existingReview;
        private int _selectedRating = 5;

        public Product Product => _product;

        private string _reviewComment = string.Empty;
        public string ReviewComment
        {
            get => _reviewComment;
            set
            {
                if (SetProperty(ref _reviewComment, value))
                {
                    ValidateReviewComment();
                    OnPropertyChanged(nameof(CanSubmitReview));
                    OnPropertyChanged(nameof(CanUpdateReview));
                }
            }
        }

        private string _reviewCommentHint = string.Empty;
        public string ReviewCommentHint
        {
            get => _reviewCommentHint;
            set => SetProperty(ref _reviewCommentHint, value);
        }

        private string _reviewCommentColor = "Red";
        public string ReviewCommentColor
        {
            get => _reviewCommentColor;
            set => SetProperty(ref _reviewCommentColor, value);
        }

        public bool HasExistingReview => _hasExistingReview;
        public bool CanSubmitReview => !_hasExistingReview && ReviewCommentColor == "Green" && SelectedRating >= 1;
        public bool CanUpdateReview => _hasExistingReview && ReviewCommentColor == "Green" && SelectedRating >= 1;

        public int SelectedRating
        {
            get => _selectedRating;
            set
            {
                var clamped = Math.Clamp(value, 1, 5);
                if (SetProperty(ref _selectedRating, clamped))
                {
                    OnPropertyChanged(nameof(CanSubmitReview));
                    OnPropertyChanged(nameof(CanUpdateReview));
                }
            }
        }

        public ICommand BackCommand { get; }
        public ICommand SubmitReviewCommand { get; }
        public ICommand UpdateReviewCommand { get; }

        public event Action? BackRequested;
        public event Action? ReviewSubmitted;
        public event Action? ReviewUpdated;

        public ProductReviewViewModel(UserSessionService sessionService,
                                    IUnitOfWork unitOfWork,
                                    Product product)
        {
            _sessionService = sessionService;
            _unitOfWork = unitOfWork;
            _product = product;

            CheckExistingReview();

            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
            SubmitReviewCommand = new RelayCommand(_ => SubmitReview(), _ => CanSubmitReview);
            UpdateReviewCommand = new RelayCommand(_ => UpdateReview(), _ => CanUpdateReview);
        }

        private void ValidateReviewComment()
        {
            if (string.IsNullOrWhiteSpace(ReviewComment))
            {
                ReviewCommentHint = "Отзыв не может быть пустым";
                ReviewCommentColor = "Red";
            }
            else if (ReviewComment.Length < 10)
            {
                ReviewCommentHint = "Отзыв должен содержать минимум 10 символов";
                ReviewCommentColor = "Red";
            }
            else
            {
                ReviewCommentHint = "✓";
                ReviewCommentColor = "Green";
            }
        }

        private void CheckExistingReview()
        {
            if (_sessionService.CurrentUser == null) return;

            _existingReview = _unitOfWork.ProductReviews
                .Find(r => r.UserId == _sessionService.CurrentUser.Id &&
                          r.ProductId == _product.Id)
                .FirstOrDefault();

            _hasExistingReview = _existingReview != null;

            OnPropertyChanged(nameof(HasExistingReview));
            OnPropertyChanged(nameof(CanSubmitReview));
            OnPropertyChanged(nameof(CanUpdateReview));

            if (_hasExistingReview)
            {
                ReviewComment = _existingReview?.Comment ?? string.Empty;
                SelectedRating = _existingReview?.Rating ?? 5;
            }
        }

        private void SubmitReview()
        {
            if (_sessionService.CurrentUser == null || _hasExistingReview) return;

            var review = new ProductReview
            {
                ProductId = _product.Id,
                UserId = _sessionService.CurrentUser.Id,
                Comment = ReviewComment,
                CreatedAt = DateTime.Now,
                Rating = SelectedRating
            };

            _unitOfWork.ProductReviews.Add(review);
            _unitOfWork.SaveChanges();

            MessageBox.Show("Спасибо за ваш отзыв!", "Отзыв отправлен",
                          MessageBoxButton.OK, MessageBoxImage.Information);

            ReviewSubmitted?.Invoke();
            BackRequested?.Invoke();
        }

        private void UpdateReview()
        {
            if (_sessionService.CurrentUser == null || !_hasExistingReview || _existingReview == null) return;

            _existingReview.Comment = ReviewComment;
            _existingReview.CreatedAt = DateTime.Now;
            _existingReview.Rating = SelectedRating;

            _unitOfWork.ProductReviews.Update(_existingReview);
            _unitOfWork.SaveChanges();

            MessageBox.Show("Ваш отзыв обновлен!", "Отзыв изменен",
                          MessageBoxButton.OK, MessageBoxImage.Information);

            ReviewUpdated?.Invoke();
            BackRequested?.Invoke();
        }
    }
}