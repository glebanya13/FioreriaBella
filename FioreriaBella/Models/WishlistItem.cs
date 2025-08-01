namespace FioreriaBella.Models
{
  public class WishlistItem
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.Now;

    public string ProductName { get; set; }
  }
}
