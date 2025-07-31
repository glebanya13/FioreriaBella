using FioreriaBella.Models;
using System.Collections.Generic;

namespace FioreriaBella.Data
{
  public interface IOrderRepository
  {
    List<Order> GetAll();
    void Update(Order order);
    void Delete(int id);
  }
}
