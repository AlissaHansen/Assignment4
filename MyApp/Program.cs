using DataLayer;

var ds = new DataService();

var order = ds.GetOrder(10748);

foreach (var item in order.OrderDetails)
{
    Console.WriteLine(item.ToString());
}

