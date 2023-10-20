﻿
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
}