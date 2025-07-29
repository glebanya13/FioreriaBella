using System.Data.SqlClient;
using FioreriaBella.Models;

namespace FioreriaBella.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly FioreriaDbContext _context;

    public UserRepository()
    {
      _context = new FioreriaDbContext();
    }

    public User GetByUsername(string username)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", conn);
      cmd.Parameters.AddWithValue("@Username", username);

      using var reader = cmd.ExecuteReader();
      if (reader.Read())
      {
        return new User
        {
          Id = (int)reader["Id"],
          Username = reader["Username"].ToString(),
          Email = reader["Email"].ToString(),
          PasswordHash = reader["PasswordHash"].ToString(),
          Role = reader["Role"].ToString()
        };
      }

      return null;
    }

    public void Add(User user)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@u, @e, @p, @r)", conn);
      cmd.Parameters.AddWithValue("@u", user.Username);
      cmd.Parameters.AddWithValue("@e", user.Email);
      cmd.Parameters.AddWithValue("@p", user.PasswordHash);
      cmd.Parameters.AddWithValue("@r", user.Role);
      cmd.ExecuteNonQuery();
    }

    public bool Exists(string username)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", conn);
      cmd.Parameters.AddWithValue("@Username", username);
      return (int)cmd.ExecuteScalar() > 0;
    }
  }
}
