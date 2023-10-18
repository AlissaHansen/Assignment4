using System.Runtime.Intrinsics.X86;

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
}
