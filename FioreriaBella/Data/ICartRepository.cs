using FioreriaBella.Models;
using System.Collections.Generic;

namespace FioreriaBella.Data
{
  public interface ICartRepository
  {
    List<CartItem> GetAll(int userId);
    void Add(CartItem item);
    void Remove(int id);
    void Clear(int userId);
  }
}
