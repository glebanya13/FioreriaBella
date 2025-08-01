using FioreriaBella.Data;
using FioreriaBella.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class WishlistPage : Page
  {
    private readonly WishlistRepository _wishlistRepo;
    private readonly ProductRepository _productRepo;
    private readonly int _userId;

    public WishlistPage(int userId)
    {
      InitializeComponent();
      _userId = userId;
      _wishlistRepo = new WishlistRepository();
      _productRepo = new ProductRepository();

      LoadWishlist();
    }

    private void LoadWishlist()
    {
      var wishlistItems = _wishlistRepo.GetAll(_userId);
      var products = _productRepo.GetAll();

      var displayList = wishlistItems.Select(item => new WishlistItem
      {
        Id = item.Id,
        ProductId = item.ProductId,
        ProductName = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? "Sconosciuto",
        AddedAt = item.AddedAt
      }).ToList();

      WishlistGrid.ItemsSource = displayList;
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
      if (sender is Button btn && btn.DataContext is WishlistItem row)
      {
        var result = MessageBox.Show("Sei sicuro di voler rimuovere questo prodotto dalla lista desideri?", "Conferma", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
          _wishlistRepo.Remove(row.Id);
          LoadWishlist();
        }
      }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.GoBack();
    }
  }
}
