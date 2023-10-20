namespace DataLayer;

public class ProductWithCategoryInfo
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
    public int UnitsInStock { get; set; }
    public string CategoryName { get; set; }
}