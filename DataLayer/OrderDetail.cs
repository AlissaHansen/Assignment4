namespace DataLayer;

public class OrderDetail
{
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public int ProductId { get; set; }
    public ICollection<Product> Products { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}