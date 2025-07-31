using FioreriaBella.Models;
using System.Collections.Generic;

namespace FioreriaBella.Data
{
  public interface IWishlistRepository
  {
    List<WishlistItem> GetAll(int userId);
    void Add(WishlistItem item);
    void Remove(int id);
    void Clear(int userId);
  }
}
