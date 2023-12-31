﻿
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class DataService
{
    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }

    public Category? GetCategory(int categoryId)
    {
        var db = new NorthwindContext();
        return db.Categories.FirstOrDefault(x => x.Id == categoryId);
    }

    public bool DeleteCategory(Category category)
    {
        return DeleteCategory(category.Id);
    }

    public bool DeleteCategory(int categoryId)
    {
        var db = new NorthwindContext();
        var category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
        if (category != null)
        {
            db.Categories.Remove(category);
            //db.Remove(category);
            return db.SaveChanges() > 0;
        }

        return false;
    }

    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();
        var id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };
        db.Add(category);
        db.SaveChanges();
        return category;
    }

    public bool UpdateCategory(int Id, string name, string description)
    {
        if (Id < 0) return false;

        var db = new NorthwindContext();

        DeleteCategory(Id);

        var category = new Category
        {
            Id = Id,
            Name = name,
            Description = description
        };
        db.Add(category);
        return db.SaveChanges() > 0;
    }

    public ProductWithCategoryInfo? GetProduct(int productId)
    {
        var db = new NorthwindContext();
        return db.Products
            .Where(product => product.Id == productId)
            .Select(product => new ProductWithCategoryInfo
            {
                ProductId = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock,
                CategoryName = product.Category.Name
            })
            .FirstOrDefault();
    }

    public IList<ProductWithCategoryInfo> GetProductByCategory(int categoryId)
    {
        var db = new NorthwindContext();
        return db.Products
            .Where(product => product.CategoryId == categoryId)
            .Select(product => new ProductWithCategoryInfo
            {
                ProductId = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock,
                CategoryName = product.Category.Name
            })
            .ToList();
    }

    public IList<ProductAndCategoryInfo> GetProductByName(string searchString)
    {
        var db = new NorthwindContext();
        return db.Products
            .Where(product => product.Name.Contains(searchString))
            .Select(product => new ProductAndCategoryInfo
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock,
                CategoryName = product.Category.Name
            })
            .ToList();
    }

    public Order? GetOrder(int searchId)
    {
        var db = new NorthwindContext();

        foreach (var item in db.Orders
                     .Include(od => od.OrderDetails)
                     .ThenInclude(p => p.Product)
                     .ThenInclude(c => c.Category)
                     .Where(x => x.Id == searchId))
        {
            return item;
        }
        return null;
    }

    public IList<Order> GetOrders()
    {
        var db = new NorthwindContext();
        return db.Orders.ToList();
    }

    public IList<OrderDetails> GetOrderDetailsByOrderId(int searchId)
    {
        var db = new NorthwindContext();
        return db.OrderDetails
            .Where(x => x.OrderId == searchId)
            .Include(p => p.Product)
            .ToList();
    }

    public IList<OrderDetails> GetOrderDetailsByProductId(int searchId)
    {
        var db = new NorthwindContext();
        return db.OrderDetails
            .Where(x => x.ProductId == searchId)
            .Include(o => o.Order)
            .OrderBy(x=>x.OrderId)
            .ToList();
    }
    
    
}   