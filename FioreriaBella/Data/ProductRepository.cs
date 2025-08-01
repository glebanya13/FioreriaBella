using FioreriaBella.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FioreriaBella.Data
{
  public class ProductRepository : IProductRepository
  {
    private readonly FioreriaDbContext _context;

    public ProductRepository()
    {
      _context = new FioreriaDbContext();
    }

    public List<Product> GetAll()
    {
      var products = new List<Product>();
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("SELECT * FROM Products", conn);
      using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        products.Add(new Product
        {
          Id = (int)reader["Id"],
          Name = reader["Name"].ToString(),
          Description = reader["Description"].ToString(),
          Price = (decimal)reader["Price"],
          Stock = (int)reader["Stock"],
          ImageUrl = reader["ImageUrl"].ToString()
        });
      }
      return products;
    }

    public Product GetById(int id)
    {
      using var conn = _context.CreateConnection();
      conn.Open();

      var cmd = new SqlCommand("SELECT * FROM Products WHERE Id = @id", conn);
      cmd.Parameters.AddWithValue("@id", id);

      using var reader = cmd.ExecuteReader();
      if (reader.Read())
      {
        return new Product
        {
          Id = (int)reader["Id"],
          Name = reader["Name"].ToString(),
          Description = reader["Description"].ToString(),
          Price = (decimal)reader["Price"],
          Stock = (int)reader["Stock"],
          ImageUrl = reader["ImageUrl"].ToString()
        };
      }

      return null;
    }


    public void Add(Product product)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("INSERT INTO Products (Name, Description, Price, Stock, ImageUrl) VALUES (@n, @d, @p, @s, @i)", conn);
      cmd.Parameters.AddWithValue("@n", product.Name);
      cmd.Parameters.AddWithValue("@d", product.Description);
      cmd.Parameters.AddWithValue("@p", product.Price);
      cmd.Parameters.AddWithValue("@s", product.Stock);
      cmd.Parameters.AddWithValue("@i", product.ImageUrl ?? "");
      cmd.ExecuteNonQuery();
    }

    public void Update(Product product)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("UPDATE Products SET Name=@n, Description=@d, Price=@p, Stock=@s, ImageUrl=@i WHERE Id=@id", conn);
      cmd.Parameters.AddWithValue("@n", product.Name);
      cmd.Parameters.AddWithValue("@d", product.Description);
      cmd.Parameters.AddWithValue("@p", product.Price);
      cmd.Parameters.AddWithValue("@s", product.Stock);
      cmd.Parameters.AddWithValue("@i", product.ImageUrl ?? "");
      cmd.Parameters.AddWithValue("@id", product.Id);
      cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
      using var conn = _context.CreateConnection();
      conn.Open();
      var cmd = new SqlCommand("DELETE FROM Products WHERE Id = @id", conn);
      cmd.Parameters.AddWithValue("@id", id);
      cmd.ExecuteNonQuery();
    }
  }
}
