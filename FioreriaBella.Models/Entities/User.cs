using System.Net;
using System.Windows.Controls;

namespace FioreriaBella.Models.Entities
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsAdmin { get; set; }

    public ICollection<Cart>? Carts { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<ProductReview>? ProductReviews { get; set; }
    public ICollection<Address>? Addresses { get; set; }
  }
}
