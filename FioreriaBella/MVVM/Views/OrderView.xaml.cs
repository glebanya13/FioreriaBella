using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
    public partial class OrderView : Page
    {
        public OrderView(UserSessionService sessionService, IUnitOfWork unitOfWork)
        {
            InitializeComponent();

            var vm = new OrderViewModel(unitOfWork, sessionService);
            DataContext = vm;

            vm.OrderConfirmed += () =>
            {
                this.NavigationService?.Navigate(new UserView(sessionService, unitOfWork));
            };
        }
    }
}
