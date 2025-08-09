using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
    public partial class MyOrdersView : Page
    {
        private readonly UserSessionService _userSessionService;
        private readonly IUnitOfWork _unitOfWork;

        public MyOrdersView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
        {
            InitializeComponent();

            _userSessionService = userSessionService;
            _unitOfWork = unitOfWork;

            var vm = new MyOrdersViewModel(_unitOfWork, _userSessionService);
            DataContext = vm;

            vm.BackRequested += () => this.NavigationService?.GoBack();
        }
    }
}