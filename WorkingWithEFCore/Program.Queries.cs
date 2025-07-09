using Microsoft.EntityFrameworkCore;

using WorkingWithEFCore.Models;
partial class Program
{
    static void QueringCategories()
    {
        using (Northwind db = new())
        {
            SectionTitle("Categories and how many products they have:");

            IQueryable<Category>? categories = db.Categories?.Include(categories => categories.Products);

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found.");
                return;
            }

            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products");
            }
        }
    }

    static void FilteredIncludes()
    {
        using (Northwind db = new())
        {
            SectionTitle("Products with a minimum number of units in stock");

            string? input;
            int stock;
            do
            {
                Console.Write("Enter a minimum for units in stock: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out stock));

            IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.Stock >= stock));

            if ((categories is null) || (!categories.Any()))
            {
                Fail("No categories found");
                return;
            }

            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock");

                foreach (Product p in c.Products)
                {
                    Console.WriteLine($"\t{p.ProductName} has {p.Stock} units in stock");
                }
                Console.WriteLine(new string('-', 50));
            }
        }
    }

    static void QueryingProducts()
    {
        using (Northwind db = new())
        {
            SectionTitle("Products that cost more than a price, highest at top");

            string? input;
            decimal price;

            do
            {
                Console.Write("Enter a product price: ");
                input = Console.ReadLine();
            } while (!decimal.TryParse(input, out price));

            IQueryable<Product>? products = db.Products?.Where(product => product.Cost > price).OrderByDescending(product => product.Cost);

            if ((products is null) || (!products.Any()))
            {
                Fail("No products found.");
                return;
            }

            foreach (Product p in products)
            {
                Console.WriteLine($"{p.ProductId}: {p.ProductName} costs {p.Cost:$#,##0.00} and has {p.Stock} in Stock");
            }
        }
    }
}
