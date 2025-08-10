using FioreriaBella.DAL.Interfaces;
using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class ManageProductsViewModel : BaseViewModel
  {
    private readonly IUnitOfWork _unitOfWork;

    public ObservableCollection<Product> Products { get; }

    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand ViewReviewsCommand { get; }

    public event Action<Product> RequestEditProduct;
    public event Action<Product> RequestDeleteProduct;
    public event Action RequestAddProduct;
    public event Action RequestBack;
    public event Action<Product> RequestViewReviews;


    public ManageProductsViewModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;

      Products = new ObservableCollection<Product>(_unitOfWork.Products.GetAll());

      AddCommand = new RelayCommand(_ => RequestAddProduct?.Invoke());
      EditCommand = new RelayCommand(p => RequestEditProduct?.Invoke(p as Product));
      DeleteCommand = new RelayCommand(p => RequestDeleteProduct?.Invoke(p as Product));
      BackCommand = new RelayCommand(_ => RequestBack?.Invoke());
      ViewReviewsCommand = new RelayCommand(p => RequestViewReviews?.Invoke(p as Product));
    }

    public void RefreshProducts()
    {
      Products.Clear();
      foreach (var p in _unitOfWork.Products.GetAll())
        Products.Add(p);
    }
  }
}
