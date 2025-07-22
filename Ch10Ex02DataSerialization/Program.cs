

using Microsoft.EntityFrameworkCore;

// NiceJob();

// using (Northwind db = new())
// {
//     IQueryable<Product> products = db.Products;

//     if (products is not null)
//     {
//         foreach (Product p in products)
//         {
//             db.Entry(p).Reference(p => p.Category).Load();
//             Console.WriteLine($"Name: {p.Name} Category: {p.Category.CategoryName}");
//         }
//     }

// }



// using (Northwind db = new())
// {
//     foreach (Product p in db.Products)
//     {
//         Console.WriteLine($"Name: {p.Name} {p.Category.CategoryName}");
//     }
// }

// Console.WriteLine(new string('-', 50));

// using (Northwind db = new())
// {
//     foreach (Category c in db.Categories)
//     {
//         Console.WriteLine(c.CategoryName);
//     }
// }

// using (Northwind db = new())
// {
//     IQueryable<Category> categories = db.Categories;
//     foreach (Category c in categories)
//     {
//         Console.WriteLine(new string('-', 50));
//         foreach (Product p in c.Products)
//         {
//             Console.WriteLine(p.Name);

//         }
//     }
// }

using (Northwind db = new())
{
    IQueryable<Category> products = db.Categories;
    SerializingToXml(products, false);
}

// Нужно отключать пркоси объекты чтоб работало или делать DTO
// using (Northwind db = new())
// {
//     Product product = db.Products.First();
//     XmlSerialize(product);
// }