using FioreriaBella.Data;
using FioreriaBella.Models;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace FioreriaBella.Services
{
  public class AuthService
  {
    private readonly IUserRepository _userRepo;

    public AuthService()
    {
      _userRepo = new UserRepository();
    }

    public User Login(string username, string password)
    {
      var user = _userRepo.GetByUsername(username);
      if (user == null) return null;

      var hash = HashPassword(password);
      return user.PasswordHash == hash ? user : null;
    }

    public bool Register(string username, string email, string password, string role = "User")
    {
      if (_userRepo.Exists(username)) return false;

      var newUser = new User
      {
        Username = username,
        Email = email,
        PasswordHash = HashPassword(password),
        Role = role
      };

      _userRepo.Add(newUser);
      return true;
    }

    private string HashPassword(string password)
    {
      using var sha256 = SHA256.Create();
      var bytes = Encoding.UTF8.GetBytes(password);
      var hash = sha256.ComputeHash(bytes);
      return Convert.ToBase64String(hash);
    }
  }
}
