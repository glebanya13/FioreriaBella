using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.ViewModels;
using System.Windows;

namespace FioreriaBella.MVVM.Views.Dialogs
{
    public partial class OrderDialog : Window
    {
        public Order Order { get; private set; }

        public OrderDialog(Order existingOrder)
        {
            InitializeComponent();

            var vm = new OrderDialogViewModel(existingOrder);
            vm.RequestClose += (success, result) =>
            {
                DialogResult = success;
                if (success && result != null)
                {
                    Order = result;
                }

                Close();
            };

            DataContext = vm;
        }
    }
}
