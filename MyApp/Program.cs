using DataLayer;

var ds = new DataService();

var categories = ds.GetCategories();

foreach (var entity in categories) 
{
    Console.WriteLine(entity);
}