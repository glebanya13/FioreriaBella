using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.MVVM.Services;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
  public partial class ManageOrdersView : Page
  {
    public ManageOrdersView(UserSessionService sessionService, IUnitOfWork unitOfWork)
    {
      InitializeComponent();
      DataContext = new ManageOrdersViewModel(unitOfWork, NavigationService);
    }
  }
}
