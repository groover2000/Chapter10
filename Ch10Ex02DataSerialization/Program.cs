
using (Northwind db = new())
{
    foreach (Product p in db.Products)
    {
        Console.WriteLine(p.Name);
    }
}
