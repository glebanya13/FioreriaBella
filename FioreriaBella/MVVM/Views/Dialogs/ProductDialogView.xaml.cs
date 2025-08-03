using FioreriaBella.MVVM.ViewModels;
using System.Windows;
using FioreriaBella.Models.Entities;

namespace FioreriaBella.MVVM.Views.Dialogs
{
  public partial class ProductDialog : Window
  {
    public Product Result { get; private set; }

    public ProductDialog(Product product = null)
    {
      InitializeComponent();

      var vm = new ProductDialogViewModel(product);
      vm.RequestClose += (success, result) =>
      {
        DialogResult = success;
        if (success)
          Result = result;
        Close();
      };

      DataContext = vm;
    }
  }
}
