namespace FioreriaBella.Models.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime OrderDate { get; set; }

    public User? User { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    public ICollection<Payment>? Payments { get; set; }

    public decimal TotalAmount
    {
      get
      {
        if (OrderItems == null) return 0;
        return OrderItems.Sum(item => (item.Product?.Price ?? 0) * item.Quantity);
      }
    }

    public string CustomerName => User?.Username ?? $"ID {UserId}";
  }
}
