using FioreriaBella.DAL.Interfaces;
using FioreriaBella.MVVM.Services;
using FioreriaBella.MVVM.ViewModels;
using System.Windows.Controls;

namespace FioreriaBella.MVVM.Views
{
    public partial class ManageOrdersView : Page
    {
        public ManageOrdersView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();

            var vm = new ManageOrdersViewModel(unitOfWork);
            DataContext = vm;

            vm.BackRequested += () => this.NavigationService?.GoBack();
        }
    }
}