using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

    public event Action BackRequested;
    public event Action<Product> ProductAddedToCart;

    public CatalogViewModel()
    {
      Products = new ObservableCollection<Product>();
    }

    public CatalogViewModel(IUnitOfWork unitOfWork, UserSessionService userSessionService)
    {
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      _userSessionService = userSessionService ?? throw new ArgumentNullException(nameof(userSessionService));

      _allProducts = _unitOfWork.Products.GetAll().ToList();
      ApplyFilters();

      AddToCartCommand = new RelayCommand(AddToCart);
      BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
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
        string term = SearchText.ToLower();
        filtered = filtered.Where(p =>
          (p.Name?.ToLower().Contains(term) ?? false) ||
          (p.Description?.ToLower().Contains(term) ?? false));
      }

      filtered = SelectedSortOption switch
      {
        "Цена ↑" => filtered.OrderBy(p => p.Price),
        "Цена ↓" => filtered.OrderByDescending(p => p.Price),
        _ => filtered
      };

      Products.Clear();
      foreach (var p in filtered)
        Products.Add(p);
    }
  }
}
