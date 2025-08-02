namespace FioreriaBella.Models.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public float Rating { get; set; }
    public int Quantity { get; set; }
    public string? Picture { get; set; }
    public int? CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<Cart>? Carts { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    public ICollection<ProductReview>? ProductReviews { get; set; }
  }
}
