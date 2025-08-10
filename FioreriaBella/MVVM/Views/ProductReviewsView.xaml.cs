using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
    public partial class ProductReviewsView : Page
    {
        public ProductReviewsView(UserSessionService sessionService, IUnitOfWork unitOfWork, int productId)
        {
            InitializeComponent();
            var vm = new ProductReviewsViewModel(unitOfWork, sessionService, productId);
            DataContext = vm;
            vm.BackRequested += () => this.NavigationService?.GoBack();
        }
    }
}