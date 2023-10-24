namespace DataLayer;

public class OrderDetails
{
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    
    public Order Order { get; set; }
    
    public override string ToString()
    {
        return $"{UnitPrice}, {Quantity}, {Discount},{ProductId}, {Order}";
    }
}