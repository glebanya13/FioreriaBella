using FioreriaBella.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FioreriaBella.Data
{
  public class OrderRepository : IOrderRepository
  {
    private readonly FioreriaDbContext _context;

    public OrderRepository()
    {
      _context = new FioreriaDbContext();
    }

    public void Add(Order order)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var cmd = new SqlCommand(
          "INSERT INTO Orders (CustomerName, Address, OrderDate, TotalAmount, Status) VALUES (@n, @a, @d, @t, @s)", conn);
      cmd.Parameters.AddWithValue("@n", order.CustomerName);
      cmd.Parameters.AddWithValue("@a", order.Address);
      cmd.Parameters.AddWithValue("@d", order.OrderDate);
      cmd.Parameters.AddWithValue("@t", order.TotalAmount);
      cmd.Parameters.AddWithValue("@s", order.Status);

      cmd.ExecuteNonQuery();
    }

    public List<Order> GetAll()
    {
      var orders = new List<Order>();
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("SELECT * FROM Orders", conn);
      using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        orders.Add(new Order
        {
          Id = (int)reader["Id"],
          CustomerName = reader["CustomerName"].ToString(),
          OrderDate = (DateTime)reader["OrderDate"],
          TotalAmount = (decimal)reader["TotalAmount"],
          Status = reader["Status"].ToString()
        });
      }
      return orders;
    }

    public void Update(Order order)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("UPDATE Orders SET CustomerName=@n, OrderDate=@d, TotalAmount=@t, Status=@s WHERE Id=@id", conn);
      cmd.Parameters.AddWithValue("@n", order.CustomerName);
      cmd.Parameters.AddWithValue("@d", order.OrderDate);
      cmd.Parameters.AddWithValue("@t", order.TotalAmount);
      cmd.Parameters.AddWithValue("@s", order.Status);
      cmd.Parameters.AddWithValue("@id", order.Id);
      cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("DELETE FROM Orders WHERE Id = @id", conn);
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
    }
  }
}
