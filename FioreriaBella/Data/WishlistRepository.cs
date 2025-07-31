using System.Data.SqlClient;
using FioreriaBella.Models;

namespace FioreriaBella.Data
{
  public class WishlistRepository : IWishlistRepository
  {
    private readonly FioreriaDbContext _context;

    public WishlistRepository()
    {
      _context = new FioreriaDbContext();
    }

    public List<WishlistItem> GetAll(int userId)
    {
      var items = new List<WishlistItem>();
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("SELECT * FROM Wishlist WHERE UserId = @u", conn);
      cmd.Parameters.AddWithValue("@u", userId);
      using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        items.Add(new WishlistItem
        {
          Id = (int)reader["Id"],
          UserId = (int)reader["UserId"],
          ProductId = (int)reader["ProductId"],
          AddedAt = (DateTime)reader["AddedAt"]
        });
      }
      return items;
    }

    public void Add(WishlistItem item)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Wishlist WHERE UserId = @u AND ProductId = @p", conn);
      checkCmd.Parameters.AddWithValue("@u", item.UserId);
      checkCmd.Parameters.AddWithValue("@p", item.ProductId);
      int count = (int)checkCmd.ExecuteScalar();

      if (count > 0)
      {
        throw new InvalidOperationException("Questo prodotto è già stato aggiunto alla tua lista dei desideri.");
      }

      var cmd = new SqlCommand("INSERT INTO Wishlist (UserId, ProductId, AddedAt) VALUES (@u, @p, @a)", conn);
      cmd.Parameters.AddWithValue("@u", item.UserId);
      cmd.Parameters.AddWithValue("@p", item.ProductId);
      cmd.Parameters.AddWithValue("@a", item.AddedAt);
      cmd.ExecuteNonQuery();
    }

    public void Remove(int id)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("DELETE FROM Wishlist WHERE Id = @id", conn);
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
    }

    public void Clear(int userId)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("DELETE FROM Wishlist WHERE UserId = @u", conn);
      cmd.Parameters.AddWithValue("@u", userId);
      cmd.ExecuteNonQuery();
    }
  }
}
