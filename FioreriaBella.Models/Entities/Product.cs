using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FioreriaBella.Models.Entities
{
  public class Product : INotifyPropertyChanged
  {
    private int _id;
    private string _name = null!;
    private string? _description;
    private decimal _price;
    private int _quantity;
    private string? _picture;
    private double _averageRating;
    private int _reviewsCount;

    public int Id
    {
      get => _id;
      set => SetField(ref _id, value);
    }

    public string Name
    {
      get => _name;
      set => SetField(ref _name, value);
    }

    public string? Description
    {
      get => _description;
      set => SetField(ref _description, value);
    }

    public decimal Price
    {
      get => _price;
      set => SetField(ref _price, value);
    }

    public int Quantity
    {
      get => _quantity;
      set => SetField(ref _quantity, value);
    }

    public string? Picture
    {
      get => _picture;
      set => SetField(ref _picture, value);
    }

    [NotMapped]
    public double AverageRating
    {
      get => _averageRating;
      set => SetField(ref _averageRating, value);
    }

    [NotMapped]
    public int ReviewsCount
    {
      get => _reviewsCount;
      set => SetField(ref _reviewsCount, value);
    }

    public ICollection<Cart>? Carts { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    public ICollection<ProductReview>? ProductReviews { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(field, value)) return false;
      field = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      return true;
    }
  }
}
