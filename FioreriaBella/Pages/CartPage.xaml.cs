using FioreriaBella.Data;
using FioreriaBella.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class CartPage : Page
  {
    private readonly int _userId;
    private readonly CartRepository _cartRepo;
    private readonly ProductRepository _productRepo;
    private List<CartItem> _enrichedItems;

    public CartPage(int userId)
    {
      InitializeComponent();
      _userId = userId;
      _cartRepo = new CartRepository();
      _productRepo = new ProductRepository();
      Load_Items();
    }

    private void Load_Items()
    {
      var items = _cartRepo.GetAll(_userId);
      _enrichedItems = new List<CartItem>();

      foreach (var item in items)
      {
        var product = _productRepo.GetById(item.ProductId);
        if (product != null)
        {
          item.ProductName = product.Name;
        }

        item.PropertyChanged += Item_PropertyChanged;

        _enrichedItems.Add(item);
      }

      CartGrid.ItemsSource = _enrichedItems;
      RecalculateTotal();
    }


    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(CartItem.Quantity))
      {
        if (sender is CartItem item)
        {
          _cartRepo.UpdateQuantity(item.Id, item.Quantity);
          RecalculateTotal();
        }
      }
    }

    private void RecalculateTotal()
    {
      decimal total = _enrichedItems.Sum(i =>
      {
        var product = _productRepo.GetById(i.ProductId);
        return product != null ? product.Price * i.Quantity : 0;
      });

      TotalText.Text = $"Totale: €{total:F2}";
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

    private void Checkout_Click(object sender, RoutedEventArgs e)
    {
      NavigationService?.Navigate(new OrderPage(_userId));
    }
  }
}
