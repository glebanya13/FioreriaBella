using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using FioreriaBella.MVVM.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class CartViewModel : BaseViewModel
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionService _userSessionService;

    private ObservableCollection<Cart> _cartItems = new();
    public ObservableCollection<Cart> CartItems
    {
      get => _cartItems;
      set => SetProperty(ref _cartItems, value);
    }

    private decimal _total;
    public decimal Total
    {
      get => _total;
      set => SetProperty(ref _total, value);
    }

    public ICommand RemoveFromCartCommand { get; }
    public ICommand IncreaseQuantityCommand { get; }
    public ICommand DecreaseQuantityCommand { get; }

    public ICommand CheckoutCommand { get; }
    public ICommand BackCommand { get; }

    public event Action CheckoutRequested;
    public event Action BackRequested;

    public CartViewModel(IUnitOfWork unitOfWork, UserSessionService sessionService)
    {
      _unitOfWork = unitOfWork;
      _userSessionService = sessionService;

      RemoveFromCartCommand = new RelayCommand(RemoveFromCart);
      IncreaseQuantityCommand = new RelayCommand(IncreaseQuantity);
      DecreaseQuantityCommand = new RelayCommand(DecreaseQuantity);

      CheckoutCommand = new RelayCommand(_ => CheckoutRequested?.Invoke());
      BackCommand = new RelayCommand(_ => BackRequested?.Invoke());

      LoadCartItems();
    }

    private void LoadCartItems()
    {
      var userId = _userSessionService.CurrentUser?.Id;
      if (userId == null)
        return;

      var carts = _unitOfWork.Carts
          .GetAll()
          .Where(c => c.UserId == userId)
          .ToList();

      var productIds = carts.Select(c => c.ProductId).Distinct().ToList();

      var products = _unitOfWork.Products
          .GetAll()
          .Where(p => productIds.Contains(p.Id))
          .ToList();

      foreach (var cartItem in carts)
      {
        cartItem.Product = products.FirstOrDefault(p => p.Id == cartItem.ProductId);
      }

      CartItems = new ObservableCollection<Cart>(carts);

      foreach (var item in CartItems)
      {
        item.PropertyChanged += CartItem_PropertyChanged;
      }

      UpdateTotal();
    }

    private void CartItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(Cart.Quantity))
      {
        if (sender is Cart item)
        {
          if (item.Quantity < 1)
            item.Quantity = 1;

          _unitOfWork.Carts.Update(item);
          _unitOfWork.SaveChanges();
          UpdateTotal();
        }
      }
    }

    private void RemoveFromCart(object? parameter)
    {
      if (parameter is Cart cartItem)
      {
        CartItems.Remove(cartItem);
        _unitOfWork.Carts.Remove(cartItem);
        _unitOfWork.SaveChanges();
        UpdateTotal();
      }
    }

    private void IncreaseQuantity(object? parameter)
    {
      if (parameter is Cart cartItem)
      {
        cartItem.Quantity++;
        _unitOfWork.Carts.Update(cartItem);
        _unitOfWork.SaveChanges();
        UpdateTotal();
      }
    }

    private void DecreaseQuantity(object? parameter)
    {
      if (parameter is Cart cartItem && cartItem.Quantity > 1)
      {
        cartItem.Quantity--;
        _unitOfWork.Carts.Update(cartItem);
        _unitOfWork.SaveChanges();
        UpdateTotal();
      }
    }

    private void UpdateTotal()
    {
      Total = CartItems.Sum(item => item.Quantity * (item.Product?.Price ?? 0));
    }
  }
}
