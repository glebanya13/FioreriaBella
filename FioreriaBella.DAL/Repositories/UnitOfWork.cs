using FioreriaBella.Models.Entities;
using FioreriaBella.DAL.Interfaces;

namespace FioreriaBella.DAL.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApplicationContext _context;

    private IRepository<Product> _products;
    private IRepository<Category> _categories;
    private IRepository<User> _users;
    private IRepository<Cart> _carts;
    private IRepository<Order> _orders;
    private IRepository<OrderItem> _orderItems;
    private IRepository<ProductReview> _productReviews;
    private IRepository<Payment> _payments;
    private IRepository<Address> _addresses;

    public UnitOfWork(ApplicationContext context)
    {
      _context = context;
    }

    public IRepository<Product> Products => _products ??= new Repository<Product>(_context);
    public IRepository<Category> Categories => _categories ??= new Repository<Category>(_context);
    public IRepository<User> Users => _users ??= new Repository<User>(_context);
    public IRepository<Cart> Carts => _carts ??= new Repository<Cart>(_context);
    public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);
    public IRepository<OrderItem> OrderItems => _orderItems ??= new Repository<OrderItem>(_context);
    public IRepository<ProductReview> ProductReviews => _productReviews ??= new Repository<ProductReview>(_context);
    public IRepository<Payment> Payments => _payments ??= new Repository<Payment>(_context);
    public IRepository<Address> Addresses => _addresses ??= new Repository<Address>(_context);

    public void SaveChanges()
    {
      _context.SaveChanges();
    }
  }
}
