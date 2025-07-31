using FioreriaBella.Data;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class CatalogPage : Page
  {
    private readonly ProductRepository _repo;

    public CatalogPage()
    {
      InitializeComponent();
      _repo = new ProductRepository();
      LoadProducts();
    }

    private void LoadProducts()
    {
      ProductsList.ItemsSource = _repo.GetAll();
    }

    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button btn && btn.Tag is int productId)
      {
        MessageBox.Show($"Prodotto con ID {productId} aggiunto al carrello.");
      }
    }

    private void AddToWishlist_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button btn && btn.Tag is int productId)
      {
        MessageBox.Show($"Prodotto con ID {productId} aggiunto alla lista desideri.");
      }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null && NavigationService.CanGoBack)
      {
        NavigationService.GoBack();
      }
      else
      {
        MessageBox.Show("Non è possibile tornare indietro.");
      }
    }
  }
}
