using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FioreriaBella.DAL
{
  public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
  {
    public ApplicationContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
      optionsBuilder.UseSqlServer(
                  "Data Source=(local);Initial Catalog=FioreriaBellaDB;Integrated Security=True;TrustServerCertificate=True");

      return new ApplicationContext(optionsBuilder.Options);
    }
  }
}
