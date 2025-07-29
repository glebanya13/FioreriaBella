using System.Configuration;
using System.Data.SqlClient;

namespace FioreriaBella.Data
{
  public class FioreriaDbContext
  {
    private readonly string _connectionString;

    public FioreriaDbContext()
    {
      _connectionString = ConfigurationManager.ConnectionStrings["FioreriaDB"].ConnectionString;
    }

    public SqlConnection CreateConnection()
    {
      return new SqlConnection(_connectionString);
    }

    public bool TestConnection(out string errorMessage)
    {
      errorMessage = null;
      try
      {
        using var conn = CreateConnection();
        conn.Open();
        conn.Close();
        return true;
      }
      catch (Exception ex)
      {
        errorMessage = ex.Message;
        return false;
      }
    }
  }
}
