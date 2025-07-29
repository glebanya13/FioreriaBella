using FioreriaBella.Models;

namespace FioreriaBella.Data
{
  public interface IProductRepository
  {
    List<Product> GetAll();
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
  }
}
