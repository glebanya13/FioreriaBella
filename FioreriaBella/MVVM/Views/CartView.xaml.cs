using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.DAL.Interfaces;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class CartView : Page
  {
    public CartView(UserSessionService sessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();

      var vm = new CartViewModel(unitOfWork, sessionService);
      DataContext = vm;

      vm.CheckoutRequested += () =>
      {
        NavigationService?.Navigate(new OrderView(sessionService, unitOfWork));
      };

      vm.BackRequested += () =>
      {
        if (NavigationService?.CanGoBack == true)
          NavigationService.GoBack();
      };
    }
  }
}
