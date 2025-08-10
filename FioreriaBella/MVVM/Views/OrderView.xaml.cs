using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
    public partial class OrderView : Page
    {
        private readonly UserSessionService _sessionService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderView(UserSessionService sessionService, IUnitOfWork unitOfWork)
        {
            InitializeComponent();

            _sessionService = sessionService;
            _unitOfWork = unitOfWork;

            var vm = new OrderViewModel(_unitOfWork, _sessionService);
            DataContext = vm;

            vm.OrderConfirmed += () =>
            {
                this.NavigationService?.Navigate(new UserView(_sessionService, _unitOfWork));
            };

            vm.BackRequested += () =>
            {
                this.NavigationService?.GoBack();
            };
        }
    }
}