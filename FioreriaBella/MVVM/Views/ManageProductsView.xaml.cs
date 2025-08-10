using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.MVVM.Views.Dialogs;
using FioreriaBella.Models.Entities;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class ManageProductsView : Page
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserSessionService _userSessionService;
    private readonly ManageProductsViewModel _viewModel;

    public ManageProductsView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _unitOfWork = unitOfWork;
      _userSessionService = userSessionService;

      _viewModel = new ManageProductsViewModel(_unitOfWork);
      DataContext = _viewModel;

      _viewModel.RequestAddProduct += () =>
      {
        var dialog = new ProductDialog();
        if (dialog.ShowDialog() == true && dialog.Result is Product newProduct)
        {
          _unitOfWork.Products.Add(newProduct);
          _unitOfWork.SaveChanges();
          _viewModel.Products.Add(newProduct);
        }
      };

      _viewModel.RequestEditProduct += product =>
      {
        if (product != null)
        {
          var dialog = new ProductDialog(product);
          if (dialog.ShowDialog() == true && dialog.Result is Product updatedProduct)
          {
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;
            product.Picture = updatedProduct.Picture;

            _unitOfWork.Products.Update(product);
            _unitOfWork.SaveChanges();
          }
        }
      };

      _viewModel.RequestDeleteProduct += product =>
      {
        if (product != null && MessageBox.Show($"Удалить товар \"{product.Name}\"?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
          _unitOfWork.Products.Remove(product);
          _unitOfWork.SaveChanges();
          _viewModel.Products.Remove(product);
        }
      };

    _viewModel.RequestViewReviews += product =>
    {
        if (product != null)
        {
            var reviewsView = new ProductReviewsView(_userSessionService, _unitOfWork, product.Id);
            this.NavigationService?.Navigate(reviewsView);
        }
    };

      _viewModel.RequestBack += () =>
      {
        this.NavigationService?.GoBack();
      };
    }
  }
}
