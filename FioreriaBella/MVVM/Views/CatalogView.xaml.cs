using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class CatalogView : Page
  {
    private readonly UserSessionService _userSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public CatalogView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      _userSessionService = userSessionService;
      _unitOfWork = unitOfWork;

      var vm = new CatalogViewModel(_unitOfWork, _userSessionService);
      DataContext = vm;

      vm.BackRequested += () => this.NavigationService?.GoBack();

      vm.ProductAddedToCart += product =>
      {
        MessageBox.Show($"Товар '{product.Name}' добавлен в корзину");
      };
    }
  }
}
