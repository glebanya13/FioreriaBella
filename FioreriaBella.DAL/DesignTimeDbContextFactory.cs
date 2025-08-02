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
      optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ClickSnup;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;");

      return new ApplicationContext(optionsBuilder.Options);
    }
  }
}
