using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
    public partial class MyPaymentsView : Page
    {
        private readonly UserSessionService _userSessionService;
        private readonly IUnitOfWork _unitOfWork;

        public MyPaymentsView(UserSessionService userSessionService, IUnitOfWork unitOfWork)
        {
            InitializeComponent();

            _userSessionService = userSessionService;
            _unitOfWork = unitOfWork;

            var vm = new MyPaymentsViewModel(_unitOfWork, _userSessionService);
            DataContext = vm;

            vm.BackRequested += () => this.NavigationService?.GoBack();
        }
    }
}