using System.ComponentModel;

namespace FioreriaBella.Models
{
  public class User : INotifyPropertyChanged
  {
    private int _id;
    private string _username;
    private string _email;
    private string _passwordHash;
    private string _role;

    public int Id
    {
      get => _id;
      set { _id = value; OnPropertyChanged(nameof(Id)); }
    }

    public string Username
    {
      get => _username;
      set { _username = value; OnPropertyChanged(nameof(Username)); }
    }

    public string Email
    {
      get => _email;
      set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    public string PasswordHash
    {
      get => _passwordHash;
      set { _passwordHash = value; OnPropertyChanged(nameof(PasswordHash)); }
    }

    public string Role
    {
      get => _role;
      set { _role = value; OnPropertyChanged(nameof(Role)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
