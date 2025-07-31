using FioreriaBella.Models;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Dialogs
{
  public partial class OrderDialog : Window
  {
    public Order Order { get; private set; }

    public OrderDialog(Order order)
    {
      InitializeComponent();

      Order = new Order
      {
        Id = order.Id,
        CustomerName = order.CustomerName,
        Address = order.Address,
        OrderDate = order.OrderDate,
        TotalAmount = order.TotalAmount,
        Status = order.Status
      };

      DataContext = Order;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      if (StatusComboBox.SelectedItem is ComboBoxItem selectedItem)
      {
        Order.Status = selectedItem.Content.ToString();
        DialogResult = true;
      }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}
