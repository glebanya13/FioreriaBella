using FioreriaBella.Data;
using FioreriaBella.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class CatalogPage : Page
  {
    private readonly ProductRepository _repo;
    private readonly CartRepository _cartRepo;
    private readonly WishlistRepository _wishlistRepo;

    private readonly int _userId;
    public CatalogPage(int userId)
    {
      InitializeComponent();

      _userId = userId;

      _repo = new ProductRepository();
      _cartRepo = new CartRepository();
      _wishlistRepo = new WishlistRepository();

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
        try
        {
          var item = new CartItem
          {
            UserId = _userId,
            ProductId = productId,
            Quantity = 1,
            AddedAt = DateTime.Now
          };

          _cartRepo.Add(item);
          MessageBox.Show($"Prodotto con ID {productId} aggiunto al carrello.");
        }
        catch (InvalidOperationException ex)
        {
          MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Errore durante l'aggiunta al carrello: {ex.Message}");
        }
      }
    }

    private void AddToWishlist_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button btn && btn.Tag is int productId)
      {
        try
        {
          var item = new WishlistItem
          {
            UserId = _userId,
            ProductId = productId,
            AddedAt = DateTime.Now
          };

          _wishlistRepo.Add(item);
          MessageBox.Show($"Prodotto con ID {productId} aggiunto alla lista desideri.");
        }
        catch (InvalidOperationException ex)
        {
          MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Errore durante l'aggiunta alla lista desideri: {ex.Message}");
        }
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
