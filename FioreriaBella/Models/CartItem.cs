using System;
using System.ComponentModel;

namespace FioreriaBella.Models
{
  public class CartItem : INotifyPropertyChanged
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    private int _quantity;
    public int Quantity
    {
      get => _quantity;
      set
      {
        if (_quantity != value)
        {
          _quantity = value;
          OnPropertyChanged(nameof(Quantity));
        }
      }
    }

    public DateTime AddedAt { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
