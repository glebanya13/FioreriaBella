using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FioreriaBella.Models.Entities
{
  public class Cart : INotifyPropertyChanged
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    private int quantity;
    public int Quantity
    {
      get => quantity;
      set
      {
        if (quantity != value)
        {
          quantity = value;
          OnPropertyChanged();
        }
      }
    }

    public User? User { get; set; }
    public Product? Product { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
