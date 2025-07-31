public class Order
{
  public int Id { get; set; }
  public string CustomerName { get; set; }
  public string Address { get; set; }
  public DateTime OrderDate { get; set; }
  public decimal TotalAmount { get; set; }
  public string Status { get; set; }
}
