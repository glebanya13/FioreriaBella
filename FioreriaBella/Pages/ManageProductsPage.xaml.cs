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
      var product = ((FrameworkElement)sender).DataContext as Product;
    }

    private void DeleteRow_Click(object sender, RoutedEventArgs e)
    {
      var product = ((FrameworkElement)sender).DataContext as Product;
    }
  }
}
