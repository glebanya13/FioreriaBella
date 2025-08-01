using System.Data.SqlClient;
using FioreriaBella.Models;

namespace FioreriaBella.Data
{
  public class CartRepository : ICartRepository
  {
    private readonly FioreriaDbContext _context;

    public CartRepository()
    {
      _context = new FioreriaDbContext();
    }

    public List<CartItem> GetAll(int userId)
    {
      var items = new List<CartItem>();
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("SELECT * FROM Cart WHERE UserId = @u", conn);
      cmd.Parameters.AddWithValue("@u", userId);
      using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        items.Add(new CartItem
        {
          Id = (int)reader["Id"],
          UserId = (int)reader["UserId"],
          ProductId = (int)reader["ProductId"],
          Quantity = (int)reader["Quantity"],
          AddedAt = (DateTime)reader["AddedAt"]
        });
      }
      return items;
    }

    public void Add(CartItem item)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Cart WHERE UserId = @u AND ProductId = @p", conn);
      checkCmd.Parameters.AddWithValue("@u", item.UserId);
      checkCmd.Parameters.AddWithValue("@p", item.ProductId);
      int count = (int)checkCmd.ExecuteScalar();

      if (count > 0)
      {
        throw new InvalidOperationException("Questo prodotto è già stato aggiunto al tuo carrello.");
      }

      var cmd = new SqlCommand("INSERT INTO Cart (UserId, ProductId, Quantity, AddedAt) VALUES (@u, @p, @q, @a)", conn);
      cmd.Parameters.AddWithValue("@u", item.UserId);
      cmd.Parameters.AddWithValue("@p", item.ProductId);
      cmd.Parameters.AddWithValue("@q", item.Quantity);
      cmd.Parameters.AddWithValue("@a", item.AddedAt);
      cmd.ExecuteNonQuery();
    }

    public void UpdateQuantity(int cartItemId, int newQuantity)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var cmd = new SqlCommand("UPDATE Cart SET Quantity = @q WHERE Id = @id", conn);
      cmd.Parameters.AddWithValue("@q", newQuantity);
      cmd.Parameters.AddWithValue("@id", cartItemId);

      cmd.ExecuteNonQuery();
    }

    public void Remove(int id)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("DELETE FROM Cart WHERE Id = @id", conn);
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
    }

    public void Clear(int userId)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("DELETE FROM Cart WHERE UserId = @u", conn);
      cmd.Parameters.AddWithValue("@u", userId);
      cmd.ExecuteNonQuery();
    }
  }
}
