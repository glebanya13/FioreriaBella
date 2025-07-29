using FioreriaBella.Models;
using System.Collections.Generic;

namespace FioreriaBella.Data
{
  public interface IUserRepository
  {
    User GetByUsername(string username);
    void Add(User user);
    bool Exists(string username);
  }
}
