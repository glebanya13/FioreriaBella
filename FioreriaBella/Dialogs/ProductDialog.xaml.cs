using FioreriaBella.Models;
using System.Globalization;
using System.Windows;

namespace FioreriaBella.Dialogs
{
  public partial class ProductDialog : Window
  {
    public Product Product { get; private set; }
    private readonly bool _isEdit;

    public ProductDialog(Product product = null)
    {
      InitializeComponent();
      if (product != null)
      {
        Product = new Product
        {
          Id = product.Id,
          Name = product.Name,
          Description = product.Description,
          Price = product.Price,
          Stock = product.Stock,
          ImageUrl = product.ImageUrl
        };
        _isEdit = true;
      }
      else
      {
        Product = new Product();
        _isEdit = false;
      }
      DataContext = Product;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
      bool isValid = true;
      NameError.Text = "";
      DescriptionError.Text = "";
      PriceError.Text = "";
      StockError.Text = "";
      ImageUrlError.Text = "";

      if (string.IsNullOrWhiteSpace(NameTextBox.Text))
      {
        NameError.Text = "Inserisci il nome.";
        isValid = false;
      }
      if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
      {
        DescriptionError.Text = "Inserisci la descrizione.";
        isValid = false;
      }
      decimal price = 0;
      if (string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
          !decimal.TryParse(PriceTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out price) ||
          price < 0)
      {
        PriceError.Text = "Prezzo non valido.";
        isValid = false;
      }
      int stock = 0;
      if (string.IsNullOrWhiteSpace(StockTextBox.Text) ||
          !int.TryParse(StockTextBox.Text, out stock) ||
          stock < 0)
      {
        StockError.Text = "Quantità non valida.";
        isValid = false;
      }
      if (string.IsNullOrWhiteSpace(ImageUrlTextBox.Text))
      {
        ImageUrlError.Text = "Inserisci l'URL dell'immagine.";
        isValid = false;
      }

      if (!isValid)
        return;

      Product.Name = NameTextBox.Text.Trim();
      Product.Description = DescriptionTextBox.Text.Trim();
      Product.Price = price;
      Product.Stock = stock;
      Product.ImageUrl = ImageUrlTextBox.Text.Trim();

      DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}
