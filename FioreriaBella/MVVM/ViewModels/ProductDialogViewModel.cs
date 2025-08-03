using FioreriaBella.Models.Entities;
using FioreriaBella.MVVM.Commands;
using System.Globalization;
using System.Windows.Input;

namespace FioreriaBella.MVVM.ViewModels
{
  public class ProductDialogViewModel : BaseViewModel
  {
    private readonly Product? _original;

    private string name;
    private string description;
    private string price;
    private string quantity;
    private string picture;

    private string nameValidationHint;
    private string priceValidationHint;
    private string quantityValidationHint;

    private string nameValidationColor = "Red";
    private string priceValidationColor = "Red";
    private string quantityValidationColor = "Red";

    public string Name
    {
      get => name;
      set
      {
        SetProperty(ref name, value);
        ValidateName();
      }
    }

    public string Description
    {
      get => description;
      set => SetProperty(ref description, value);
    }

    public string Price
    {
      get => price;
      set
      {
        SetProperty(ref price, value);
        ValidatePrice();
      }
    }

    public string Quantity
    {
      get => quantity;
      set
      {
        SetProperty(ref quantity, value);
        ValidateQuantity();
      }
    }

    public string Picture
    {
      get => picture;
      set => SetProperty(ref picture, value);
    }

    public string NameValidationHint
    {
      get => nameValidationHint;
      set => SetProperty(ref nameValidationHint, value);
    }

    public string PriceValidationHint
    {
      get => priceValidationHint;
      set => SetProperty(ref priceValidationHint, value);
    }

    public string QuantityValidationHint
    {
      get => quantityValidationHint;
      set => SetProperty(ref quantityValidationHint, value);
    }

    public string NameValidationColor
    {
      get => nameValidationColor;
      set => SetProperty(ref nameValidationColor, value);
    }

    public string PriceValidationColor
    {
      get => priceValidationColor;
      set => SetProperty(ref priceValidationColor, value);
    }

    public string QuantityValidationColor
    {
      get => quantityValidationColor;
      set => SetProperty(ref quantityValidationColor, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event Action<bool, Product?> RequestClose;

    public ProductDialogViewModel(Product? product = null)
    {
      _original = product;

      name = product?.Name ?? "";
      description = product?.Description ?? "";
      price = product?.Price.ToString(CultureInfo.InvariantCulture) ?? "0";
      quantity = product?.Quantity.ToString() ?? "0";
      picture = product?.Picture ?? "";

      SaveCommand = new RelayCommand(_ => Save());
      CancelCommand = new RelayCommand(_ => Cancel());

      ValidateName();
      ValidatePrice();
      ValidateQuantity();
    }

    private void ValidateName()
    {
      if (string.IsNullOrWhiteSpace(Name))
      {
        NameValidationHint = "Название обязательно";
        NameValidationColor = "Red";
      }
      else if (Name.Length < 3)
      {
        NameValidationHint = "Слишком короткое имя";
        NameValidationColor = "Red";
      }
      else
      {
        NameValidationHint = "✓";
        NameValidationColor = "Green";
      }
    }

    private void ValidatePrice()
    {
      if (!decimal.TryParse(Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) || value < 0)
      {
        PriceValidationHint = "Некорректная цена";
        PriceValidationColor = "Red";
      }
      else
      {
        PriceValidationHint = "✓";
        PriceValidationColor = "Green";
      }
    }

    private void ValidateQuantity()
    {
      if (!int.TryParse(Quantity, out var value) || value < 0)
      {
        QuantityValidationHint = "Некорректное количество";
        QuantityValidationColor = "Red";
      }
      else
      {
        QuantityValidationHint = "✓";
        QuantityValidationColor = "Green";
      }
    }

    private void Save()
    {
      ValidateName();
      ValidatePrice();
      ValidateQuantity();

      if (NameValidationColor != "Green" ||
          PriceValidationColor != "Green" ||
          QuantityValidationColor != "Green")
      {
        return;
      }

      if (!decimal.TryParse(Price, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedPrice)) return;
      if (!int.TryParse(Quantity, out var parsedQuantity)) return;

      var product = new Product
      {
        Id = _original?.Id ?? 0,
        Name = Name.Trim(),
        Description = Description?.Trim(),
        Price = parsedPrice,
        Quantity = parsedQuantity,
        Picture = Picture?.Trim(),
      };

      RequestClose?.Invoke(true, product);
    }

    private void Cancel()
    {
      RequestClose?.Invoke(false, null);
    }
  }
}
