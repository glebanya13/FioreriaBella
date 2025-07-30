using System.Collections.Generic;
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

    public IEnumerable<User> GetAll()
    {
      var users = new List<User>();

      using var conn = _context.CreateConnection();
      conn.Open();

      var cmd = new SqlCommand("SELECT * FROM Users ORDER BY Username", conn);

      using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        users.Add(new User
        {
          Id = (int)reader["Id"],
          Username = reader["Username"].ToString(),
          Email = reader["Email"].ToString(),
          PasswordHash = reader["PasswordHash"].ToString(),
          Role = reader["Role"].ToString()
        });
      }

      return users;
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

      var cmd = new SqlCommand(
        "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, @Role)",
        conn);

      cmd.Parameters.AddWithValue("@Username", user.Username);
      cmd.Parameters.AddWithValue("@Email", user.Email);
      cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
      cmd.Parameters.AddWithValue("@Role", user.Role);

      cmd.ExecuteNonQuery();
    }

    public void Update(User user)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var cmd = new SqlCommand(
        "UPDATE Users SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role WHERE Id = @Id",
        conn);

      cmd.Parameters.AddWithValue("@Username", user.Username);
      cmd.Parameters.AddWithValue("@Email", user.Email);
      cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
      cmd.Parameters.AddWithValue("@Role", user.Role);
      cmd.Parameters.AddWithValue("@Id", user.Id);

      cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var cmd = new SqlCommand("DELETE FROM Users WHERE Id = @Id", conn);
      cmd.Parameters.AddWithValue("@Id", id);

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
