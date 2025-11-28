using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace FioreriaBella.MVVM.ViewModels
{
    public class CatalogViewModel : BaseViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserSessionService _userSessionService;

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                    ApplyFilters();
            }
        }

        private string _selectedSortOption = "Без сортировки";
        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                if (SetProperty(ref _selectedSortOption, value))
                    ApplyFilters();
            }
        }

        public ObservableCollection<Product> Products { get; } = new();
        private List<Product> _allProducts = new();

        public ICommand AddToCartCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand ViewReviewsCommand { get; }

        public event Action BackRequested;
        public event Action<Product> ProductAddedToCart;
        public event Action<int> ViewReviewsRequested;

        public CatalogViewModel(IUnitOfWork unitOfWork, UserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userSessionService = userSessionService ?? throw new ArgumentNullException(nameof(userSessionService));

            LoadProducts();

            AddToCartCommand = new RelayCommand(AddToCart);
            BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
            ViewReviewsCommand = new RelayCommand(ViewReviews);
        }

        private void LoadProducts()
        {
            _allProducts = _unitOfWork.Products.GetAll().ToList();

            var reviewStats = _unitOfWork.ProductReviews
                .GetAll()
                .GroupBy(r => r.ProductId)
                .ToDictionary(
                    g => g.Key,
                    g => new
                    {
                        Average = g.Average(r => r.Rating),
                        Count = g.Count()
                    });

            foreach (var product in _allProducts)
            {
                if (reviewStats.TryGetValue(product.Id, out var stats))
                {
                    product.ReviewsCount = stats.Count;
                    product.AverageRating = Math.Round(stats.Average, 1);
                }
                else
                {
                    product.ReviewsCount = 0;
                    product.AverageRating = 0;
                }
            }

            ApplyFilters();
        }

        private void AddToCart(object parameter)
        {
            if (parameter is not Product product)
                return;

            var userId = _userSessionService.CurrentUser?.Id;
            if (userId == null)
                return;

            var existing = _unitOfWork.Carts
                .GetAll()
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == product.Id);

            if (existing != null)
            {
                System.Windows.MessageBox.Show(
                    $"Товар \"{product.Name}\" уже в корзине.",
                    "Информация",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
                return;
            }

            var cartItem = new Cart
            {
                UserId = userId.Value,
                ProductId = product.Id,
                Quantity = 1
            };

            _unitOfWork.Carts.Add(cartItem);
            _unitOfWork.SaveChanges();

            ProductAddedToCart?.Invoke(product);
        }

        private void ApplyFilters()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(p =>
                    p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    (p.Description != null && p.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }

            IOrderedEnumerable<Product> sorted;
            switch (SelectedSortOption)
            {
                case "Цена ↑":
                    sorted = filtered.OrderBy(p => p.Price);
                    break;
                case "Цена ↓":
                    sorted = filtered.OrderByDescending(p => p.Price);
                    break;
                case "Рейтинг ↑":
                    sorted = filtered
                        .OrderBy(p => p.ReviewsCount > 0 ? 0 : 1)
                        .ThenBy(p => p.AverageRating)
                        .ThenBy(p => p.ReviewsCount);
                    break;
                case "Рейтинг ↓":
                    sorted = filtered
                        .OrderBy(p => p.ReviewsCount > 0 ? 0 : 1)
                        .ThenByDescending(p => p.AverageRating)
                        .ThenByDescending(p => p.ReviewsCount);
                    break;
                default:
                    sorted = filtered.OrderBy(p => p.Id);
                    break;
            }

            Products.Clear();
            foreach (var product in sorted)
            {
                Products.Add(product);
            }
        }

        private void ViewReviews(object parameter)
        {
            if (parameter is Product product)
            {
                ViewReviewsRequested?.Invoke(product.Id);
            }
        }
    }
}