using FioreriaBella.Data;
using FioreriaBella.Dialogs;
using FioreriaBella.Models;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class ManageProductsPage : Page
  {
    private readonly ProductRepository _repo;

    public ManageProductsPage()
    {
      InitializeComponent();
      _repo = new ProductRepository();
      LoadProducts();
    }

    private void LoadProducts()
    {
      ProductsGrid.ItemsSource = _repo.GetAll();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new ProductDialog();
      if (dialog.ShowDialog() == true)
      {
        _repo.Add(dialog.Product);
        LoadProducts();
      }
    }

    private void EditRow_Click(object sender, RoutedEventArgs e)
    {
      if (((FrameworkElement)sender).DataContext is Product selectedProduct)
      {
        var dialog = new ProductDialog(selectedProduct);
        if (dialog.ShowDialog() == true)
        {
          _repo.Update(dialog.Product);
          LoadProducts();
        }
      }
    }

    private void DeleteRow_Click(object sender, RoutedEventArgs e)
    {
      if (((FrameworkElement)sender).DataContext is Product selectedProduct)
      {
        var result = MessageBox.Show($"Vuoi eliminare il prodotto \"{selectedProduct.Name}\"?",
                                     "Conferma eliminazione",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
          _repo.Delete(selectedProduct.Id);
          LoadProducts();
        }
      }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
      if (this.NavigationService != null && this.NavigationService.CanGoBack)
      {
        this.NavigationService.GoBack();
      }
      else
      {
        MessageBox.Show("Non è possibile tornare indietro.");
      }
    }

  }
}
