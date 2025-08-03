using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FioreriaBella.Models.Entities
{
  public class User : INotifyPropertyChanged
  {
    private int _id;
    private string _username = null!;
    private string _email = null!;
    private string _password = null!;
    private bool _isAdmin;

    public int Id
    {
      get => _id;
      set => SetField(ref _id, value);
    }

    public string Username
    {
      get => _username;
      set => SetField(ref _username, value);
    }

    public string Email
    {
      get => _email;
      set => SetField(ref _email, value);
    }

    public string Password
    {
      get => _password;
      set => SetField(ref _password, value);
    }

    public bool IsAdmin
    {
      get => _isAdmin;
      set => SetField(ref _isAdmin, value);
    }

    public ICollection<Cart>? Carts { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<ProductReview>? ProductReviews { get; set; }
    public ICollection<Address>? Addresses { get; set; }

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
