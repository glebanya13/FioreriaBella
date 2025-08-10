using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using System.Windows.Controls;
using FioreriaBella.MVVM.ViewModels;
using FioreriaBella.Models.Entities;

namespace FioreriaBella.MVVM.Views
{
    public partial class ProductReviewView : Page
    {
        private readonly UserSessionService _userSessionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Product _product;

        public ProductReviewView(UserSessionService userSessionService, IUnitOfWork unitOfWork, Product product)
        {
            InitializeComponent();
            _userSessionService = userSessionService;
            _unitOfWork = unitOfWork;
            _product = product;

            var vm = new ProductReviewViewModel(_userSessionService, _unitOfWork, _product);
            DataContext = vm;

            vm.BackRequested += () => this.NavigationService?.GoBack();
        }
    }
}