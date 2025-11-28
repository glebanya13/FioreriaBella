using Microsoft.EntityFrameworkCore;
using FioreriaBella.Models.Entities;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.DAL.Repositories;

namespace FioreriaBella.DAL
{
  public class ApplicationContext : DbContext
  {
    private static ApplicationContext _instance;
    private static readonly object _lock = new object();
    private IUnitOfWork _unitOfWork;

    public static ApplicationContext Instance
    {
      get
      {
        if (_instance == null)
        {
          lock (_lock)
          {
            if (_instance == null)
            {
              var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
              optionsBuilder.UseSqlServer(
                  "Data Source=(local);Initial Catalog=FioreriaBellaDB;Integrated Security=True;TrustServerCertificate=True");
              _instance = new ApplicationContext(optionsBuilder.Options);
            }
          }
        }
        return _instance;
      }
    }

    public IUnitOfWork UnitOfWork => _unitOfWork ??= new UnitOfWork(this);
    public User CurrentUser { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
      Database.EnsureCreated();
      AddAdminIfNeeded();
    }

    private ApplicationContext() : base()
    {
      Database.EnsureCreated();
      AddAdminIfNeeded();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>(entity =>
      {
        entity.HasKey(u => u.Id);
        entity.HasIndex(u => u.Username).IsUnique();
        entity.Property(u => u.Email).IsRequired();
        entity.Property(u => u.Password).IsRequired();
        entity.Property(u => u.IsAdmin).HasDefaultValue(false);

        entity.HasMany(u => u.Carts).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
        entity.HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
        entity.HasMany(u => u.ProductReviews).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
        entity.HasMany(u => u.Addresses).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
        entity.Property(p => p.Description).HasMaxLength(1000);
        entity.Property(p => p.Price).IsRequired();
        entity.Property(p => p.Quantity).IsRequired();
        entity.Property(p => p.Picture).HasMaxLength(500);

        entity.HasMany(p => p.ProductReviews).WithOne(r => r.Product).HasForeignKey(r => r.ProductId).OnDelete(DeleteBehavior.Cascade);
        entity.HasMany<OrderItem>().WithOne(oi => oi.Product).HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.Cascade);
        entity.HasMany<Cart>().WithOne(c => c.Product).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Cart>(entity =>
      {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Quantity).IsRequired();
      });

      modelBuilder.Entity<Order>(entity =>
      {
        entity.HasKey(o => o.Id);
        entity.Property(o => o.Status).IsRequired().HasMaxLength(50);
        entity.Property(o => o.OrderDate).IsRequired().HasDefaultValueSql("GETDATE()");
        entity.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
        entity.HasMany(o => o.Payments).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<OrderItem>(entity =>
      {
        entity.HasKey(oi => oi.Id);
        entity.Property(oi => oi.Quantity).IsRequired();
      });

      modelBuilder.Entity<ProductReview>(entity =>
      {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.Comment).HasMaxLength(1000);
        entity.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        entity.Property(r => r.Rating).IsRequired();
      });

      modelBuilder.Entity<Address>(entity =>
      {
        entity.HasKey(a => a.Id);
        entity.Property(a => a.Street).HasMaxLength(255);
        entity.Property(a => a.City).HasMaxLength(100);
        entity.Property(a => a.PostalCode).HasMaxLength(20);
        entity.Property(a => a.Country).HasMaxLength(100);
      });

      modelBuilder.Entity<Payment>(entity =>
      {
        entity.HasKey(p => p.Id);
        entity.Property(p => p.PaymentDate).IsRequired().HasDefaultValueSql("GETDATE()");
        entity.Property(p => p.Amount).IsRequired();
        entity.Property(p => p.PaymentMethod).HasMaxLength(50);
      });
    }

    private void AddAdminIfNeeded()
    {
      if (!Users.Any(u => u.IsAdmin))
      {
        Users.Add(new User
        {
          Username = "Admin",
          Password = "Admin1234",
          Email = "admin@mail.ru",
          IsAdmin = true
        });
        SaveChanges();
      }
    }
  }
}
