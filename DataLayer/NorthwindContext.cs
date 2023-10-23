
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class NorthwindContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public DbSet<OrderDetail> OrderDetails { get; set; }
    
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("host=localhost;db=northwind; uid=postgres; pwd=chili321");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");

        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        
        modelBuilder.Entity<OrderDetail>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetail>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetail>().Property(x => x.Quantity).HasColumnName("quantity");
        modelBuilder.Entity<OrderDetail>().Property(x => x.Discount).HasColumnName("discount");
        modelBuilder.Entity<OrderDetail>().Property(x => x.ProductId).HasColumnName("productid");
        modelBuilder.Entity<OrderDetail>().Property(x => x.OrderId).HasColumnName("orderid");
        modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.ProductId, x.OrderId });
        
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Order>().Property(x => x.Shipped).HasColumnName("shippeddate");
        modelBuilder.Entity<Order>().Property(x => x.Freight).HasColumnName("freight");
        modelBuilder.Entity<Order>().Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>().Property(x => x.ShipCity).HasColumnName("shipcity");
        
        
    }
}