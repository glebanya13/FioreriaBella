using FioreriaBella.Data;
using FioreriaBella.Models;
using FioreriaBella.Dialogs;
using System.Windows;
using System.Windows.Controls;

namespace FioreriaBella.Pages
{
  public partial class ManageOrdersPage : Page
  {
    private readonly OrderRepository _repo;

    public ManageOrdersPage()
    {
      InitializeComponent();
      _repo = new OrderRepository();
      LoadOrders();
    }

    private void LoadOrders()
    {
      OrdersGrid.ItemsSource = _repo.GetAll();
    }

    private void EditRow_Click(object sender, RoutedEventArgs e)
    {
      if (((FrameworkElement)sender).DataContext is Order selectedOrder)
      {
        var dialog = new OrderDialog(selectedOrder);
        if (dialog.ShowDialog() == true)
        {
          _repo.Update(dialog.Order);
          LoadOrders();
        }
      }
    }

    private void DeleteRow_Click(object sender, RoutedEventArgs e)
    {
      if (((FrameworkElement)sender).DataContext is Order selectedOrder)
      {
        var result = MessageBox.Show($"Vuoi eliminare l'ordine di {selectedOrder.CustomerName}?",
                                     "Conferma eliminazione",
                                     MessageBoxButton.YesNo,
                                     MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
          _repo.Delete(selectedOrder.Id);
          LoadOrders();
        }
      }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
      if (NavigationService != null && NavigationService.CanGoBack)
        NavigationService.GoBack();
      else
        MessageBox.Show("Non è possibile tornare indietro.");
    }
  }
}
