using System.Collections.ObjectModel;
using FioreriaBella.Models.Entities;

namespace FioreriaBella.MVVM.Services
{
  public class UserSessionService
  {
    public User CurrentUser { get; set; }


    public event System.Action UserChanged;

    public void SetUser(User user)
    {
      CurrentUser = user;
      UserChanged?.Invoke();
    }

    public void ClearUser()
    {
      CurrentUser = null;
      UserChanged?.Invoke();
    }
  }
}
