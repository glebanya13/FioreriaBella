using FioreriaBella.Models.Entities;

namespace FioreriaBella.DAL.Interfaces
{
  public interface IUnitOfWork
  {
    IRepository<Product> Products { get; }
    IRepository<User> Users { get; }
    IRepository<Cart> Carts { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    IRepository<ProductReview> ProductReviews { get; }
    IRepository<Payment> Payments { get; }
    IRepository<Address> Addresses { get; }
    void SaveChanges();
  }
}
