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

    public ObservableCollection<Product> Products { get; }

    public ICommand AddToCartCommand { get; }
    public ICommand AddToWishlistCommand { get; }
    public ICommand BackCommand { get; }

    public event Action BackRequested;
    public event Action<Product> ProductAddedToCart;
    public event Action<Product> ProductAddedToWishlist;

    public CatalogViewModel()
    {
      Products = new ObservableCollection<Product>();
    }

    public CatalogViewModel(IUnitOfWork unitOfWork, UserSessionService userSessionService)
    {
      _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
      _userSessionService = userSessionService ?? throw new ArgumentNullException(nameof(userSessionService));

      Products = new ObservableCollection<Product>(_unitOfWork.Products.GetAll().ToList());

      AddToCartCommand = new RelayCommand(AddToCart);
      AddToWishlistCommand = new RelayCommand(AddToWishlist);
      BackCommand = new RelayCommand(_ => BackRequested?.Invoke());
    }

    private void AddToCart(object parameter)
    {
      if (parameter is Product product)
      {
        ProductAddedToCart?.Invoke(product);
      }
    }

    private void AddToWishlist(object parameter)
    {
      if (parameter is Product product)
      {
        ProductAddedToWishlist?.Invoke(product);
      }
    }
  }
}
