using FioreriaBella.Data;
using FioreriaBella.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class OrderPage : Page
  {
    private readonly int _userId;
    private readonly CartRepository _cartRepo;
    private readonly ProductRepository _productRepo;
    private readonly OrderRepository _orderRepo;

    public OrderPage(int userId)
    {
      InitializeComponent();
      _userId = userId;
      _cartRepo = new CartRepository();
      _productRepo = new ProductRepository();
      _orderRepo = new OrderRepository();
      LoadSummary();
    }

    private void LoadSummary()
    {
      var items = _cartRepo.GetAll(_userId);
      if (!items.Any())
      {
        SummaryText.Text = "Il carrello è vuoto.";
        return;
      }

      decimal total = 0;
      var summaryLines = items.Select(item =>
      {
        var product = _productRepo.GetById(item.ProductId);
        if (product == null) return null;
        var lineTotal = product.Price * item.Quantity;
        total += lineTotal;
        return $"{product.Name} x {item.Quantity} = €{lineTotal:F2}";
      }).Where(l => l != null);

      SummaryText.Text = string.Join("\n", summaryLines) + $"\n\nTotale: €{total:F2}";
    }

    private void Confirm_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(NameBox.Text))
        {
          MessageBox.Show("Per favore, inserisci il nome.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
          return;
        }
        if (string.IsNullOrWhiteSpace(AddressBox.Text))
        {
          MessageBox.Show("Per favore, inserisci l'indirizzo.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
          return;
        }

        var items = _cartRepo.GetAll(_userId);
        if (!items.Any())
        {
          MessageBox.Show("Il carrello è vuoto.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
          return;
        }

        decimal total = 0;
        foreach (var item in items)
        {
          var product = _productRepo.GetById(item.ProductId);
          if (product == null)
          {
            MessageBox.Show($"Prodotto con ID {item.ProductId} non trovato.", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
          }
          total += product.Price * item.Quantity;
        }

        var order = new Order
        {
          CustomerName = NameBox.Text.Trim(),
          Address = AddressBox.Text.Trim() == string.Empty ? "—" : AddressBox.Text.Trim(),
          OrderDate = DateTime.Now,
          TotalAmount = total,
          Status = "In lavorazione"
        };

        _orderRepo.Add(order);
        _cartRepo.Clear(_userId);

        MessageBox.Show($"Ordine effettuato con successo!\nImporto totale: €{total:F2}", "Successo", MessageBoxButton.OK, MessageBoxImage.Information);

        NameBox.Text = "";
        AddressBox.Text = "";
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Errore durante l'inserimento dell'ordine:\n{ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
