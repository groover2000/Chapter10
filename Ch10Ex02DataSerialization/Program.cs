
using (Northwind db = new())
{
    foreach (Product p in db.Products)
    {
        Console.WriteLine(p.Name);
    }
}

Console.WriteLine(new string('-', 50));

using (Northwind db = new())
{
    foreach (Category c in db.Categories)
    {
        Console.WriteLine(c.CategoryName);
    }
}
